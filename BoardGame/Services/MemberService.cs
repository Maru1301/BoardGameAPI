using BoardGame.Models.DTOs;
using BoardGame.Infrastractures;
using BoardGame.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MongoDB.Bson;

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
        public string Register(MemberRegisterDTO dto, string confirmationUrlTemplate)
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

            MemberDTO entity = _memberRepository.SearchByAccount(dto.Account) ?? throw new Exception("Member doesn't exist!");

            // Generate confirmation URL
            string url = $"{confirmationUrlTemplate}/{entity.Id}/{dto.ConfirmCode}";

            // Send confirmation email
            new EmailHelper(_configuration).SendConfirmRegisterEmail(url, dto.Name!, dto.Email!);

            return "Registration successful! Confirmation email sent!";
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
    }

    public class MemberServiceException : Exception
    {
        public MemberServiceException(string message) : base(message){}
    }
}
