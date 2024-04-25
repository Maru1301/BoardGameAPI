using BoardGame.Models.DTOs;
using BoardGame.Infrastractures;
using Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using BoardGame.Services.Interfaces;
using BoardGame.Repositories.Interfaces;

namespace BoardGame.Services
{
    public class MemberService : IService, IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IConfiguration _configuration;
        private readonly IRepository _repository;

        public MemberService(IMemberRepository repo, IConfiguration configuration, IRepository repository)
        {
            _memberRepository = repo;
            _configuration = configuration;
            _repository = repository;
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
        public async Task<string> Register(RegisterDTO dto, string confirmationUrlTemplate)
        {
            using var transaction = await _memberRepository.GetContext().Database.BeginTransactionAsync();
            try
            {
                if (_memberRepository.CheckAccountExist(dto.Account))
                {
                    throw new Exception("Account already exists");
                }
                if (_memberRepository.CheckNameExist(dto.Name))
                {
                    throw new Exception("Name already exists");
                }
                if (_memberRepository.CheckEmailExist(dto.Email))
                {
                    throw new Exception("Email already exists");
                }

                //create a new confirm code
                dto.ConfirmCode = Guid.NewGuid().ToString("N");

                _memberRepository.Register(dto);

                MemberDTO entity = await _memberRepository.SearchByAccount(dto.Account) ?? throw new Exception("Member doesn't exist!");

                // Generate confirmation URL
                string url = $"{confirmationUrlTemplate}?memberId={entity.Id}&confirmCode={dto.ConfirmCode}";

                // Send confirmation email
                new EmailHelper(_configuration).SendConfirmRegisterEmail(url, dto.Name!, dto.Email!);

                transaction.Commit();

                return "Registration successful! Confirmation email sent!";
            }
            catch (Exception)
            {
                transaction.Rollback(); // Roll back the transaction on error
                throw; // Re-throw the exception for handling in the controller
            }
        }

        /// <summary>
        /// Activates a member's registration by verifying their ID and confirmation code.
        /// Throws an exception if the member is not found or the code is wrong.
        /// </summary>
        /// <param name="memberId">The ID of the member to activate.</param>
        /// <param name="confirmCode">The confirmation code provided by the user.</param>
        /// <returns>"Activation Succeed" or "Wrong confirm code!" based on the verification.</returns>
        /// <exception cref="Exception">Thrown if the member is not found.</exception>
        public string ActivateRegistration(string memberId, string confirmCode)
        {
            MemberDTO entity = _memberRepository.SearchById(memberId) ?? throw new Exception("Member doesn't exist!");

            if (string.Compare(entity.ConfirmCode, confirmCode) != 0) return "Wrong confirm code!";

            _memberRepository.ActivateRegistration(memberId);
            return "Activation successful";
        }

        public async Task<string> ValidateUser(LoginDTO dto)
        {
            var member = await _memberRepository.SearchByAccount(dto.Account);
            if (member == null || !ValidatePassword(member, dto.Password))
            {
                return string.Empty;
            }

            return member.IsConfirmed ? Roles.Member : Roles.Guest;
        }

        private static bool ValidatePassword(MemberDTO member, string password)
        {
            return HashUtility.ToSHA256(password, member.Salt) == member.EncryptedPassword;
        }

        public async Task<string> GenerateToken(string account)
        {
            // 生成JWT令牌
            var member = await _memberRepository.SearchByAccount(account) ?? throw new MemberServiceException("Member doesn't exist!");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"] ?? string.Empty));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(1);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, member.Id.ToString()),
                new Claim(ClaimTypes.Email, member.Email),
                new Claim(ClaimTypes.NameIdentifier, member.Account)
                // 在这里可以添加更多的claim
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:ValidIssuer"],
                audience: _configuration["JwtSettings:ValidAudience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public  IEnumerable<MemberDTO> ListMembers()
        {
            return _memberRepository.GetAll();
        }
        public async Task<MemberDTO> GetMemberInfo(string account) 
        {
            var dto = await _memberRepository.SearchByAccount(account);
            return dto ?? throw new MemberServiceException("Member doesn't exist!");
        }
    }

    public class MemberServiceException : Exception
    {
        public MemberServiceException(string message) : base(message){}
    }
}
