using BoardGame.Models.DTOs;
using BoardGame.Infrastractures;
using BoardGame.Repositories;

namespace BoardGame.Services
{
    public class MemberService : IService, IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IConfiguration _configuration;

        public MemberService(IMemberRepository repo, IConfiguration configuration)
        {
            _memberRepository = repo;
            _configuration = configuration;
        }

        /// <summary>
        /// Registers a new user based on the provided MemberRegisterDTO details.
        /// </summary>
        /// <param name="dto">The MemberRegisterDTO object containing user registration information.</param>
        /// <param name="confirmationUrlTemplate">The template for generating the confirmation URL.</param>
        /// <returns>A tuple indicating success (bool) and a message (string). 
        ///  - True with "Registration successful! Confirmation email sent!" if successful. 
        ///  - False with an error message if registration fails due to duplicate account, 
        ///    name, or email.</returns>
        public (bool Success, string Message) Register(MemberRegisterDTO dto, string confirmationUrlTemplate)
        {
            //if (_memberRepository.CheckAccountExist(dto.MemberAccount))
            //{
            //    return (false, "Account already exists");
            //}
            //if (_memberRepository.CheckNameExist(dto.MemberName))
            //{
            //    return (false, "Name already exists");
            //}
            //if (_memberRepository.CheckEmailExist(dto.MemberEmail))
            //{
            //    return (false, "Email already exists");
            //}

            //create a new confirm code
            dto.ConfirmCode = Guid.NewGuid().ToString("N");

            _memberRepository.Register(dto);

            MemberDTO entity = _memberRepository.SearchByAccount(dto.Account);
            
            // Generate confirmation URL (consider using string interpolation instead of string.Format)
            string url = $"{confirmationUrlTemplate}/{entity.Id}/{dto.ConfirmCode}";

            // Send confirmation email asynchronously (assuming SendConfirmRegisterEmail is async)
            new EmailHelper(_configuration).SendConfirmRegisterEmail(url, dto.Name!, dto.Email!);

            return (true, "Registration successful! Confirmation email sent!");
        }
    }
}
