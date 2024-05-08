﻿using BoardGame.Models.DTOs;
using BoardGame.Infrastractures;
using Utilities;
using BoardGame.Services.Interfaces;
using BoardGame.Models.EFModels;

namespace BoardGame.Services
{
    public class MemberService(IUnitOfWork unitOfWork, IConfiguration configuration) : IService, IMemberService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IConfiguration _configuration = configuration;

        /// <summary>
        /// Register a new user based on the provided information in the `RegisterDTO` object.
        /// Throws specific exceptions for duplicate account, name, or email.
        /// On successful registration, a confirmation email is sent with a generated confirmation URL
        /// for account verification.
        /// </summary>
        /// <param name="dto">The `RegisterDTO` object containing user registration information.</param>
        /// <param name="confirmationUrlTemplate">The template string used to generate the confirmation URL.</param>
        /// <returns>A message indicating successful registration.</returns>
        /// <exception cref="MemberServiceException">Thrown if error occured in MemberService.</exception>
        /// <exception cref="Exception">Thrown for any other unexpected errors during registration.</exception>
        public async Task<string> Register(RegisterDTO registerDto, string confirmationUrlTemplate)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                if (!await CheckAccountExist(registerDto.Account)) throw new MemberServiceException("Account already exists");
                if (!await CheckNameExist(registerDto.Name)) throw new MemberServiceException("Name already exists");
                if (!await CheckEmailExist(registerDto.Email)) throw new MemberServiceException("Email already exists");

                //create a new confirm code
                registerDto.ConfirmCode = Guid.NewGuid().ToString("N");

                await _unitOfWork.Members.AddAsync(registerDto.ToEntity<Member>());
                await _unitOfWork.CommitTransactionAsync();

                var dto = (await _unitOfWork.Members.GetByAccountAsync(registerDto.Account) ?? throw new MemberServiceException("Member doesn't exist!")).ToDTO<MemberDTO>();
                
                // Generate confirmation URL
                string url = $"{confirmationUrlTemplate}?memberId={dto.Id}&confirmCode={dto.ConfirmCode}";

                // Send confirmation email
                new EmailHelper(_configuration).SendConfirmRegisterEmail(url, dto.Name!, dto.Email!);

                await _unitOfWork.CommitTransactionAsync();
                return "Registration successful! Confirmation email sent!";
            }
            catch (MemberAccessException)
            {
                await _unitOfWork.RollbackTransactionAsync(); // Roll back the transaction on error
                throw; // Re-throw the exception for handling in the controller
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync(); // Roll back the transaction on error
                throw; // Re-throw the exception for handling in the controller
            }
        }

        /// <summary>
        /// Activates a member's registration by verifying their ID and confirmation code.
        /// Throws specific exceptions for member not found, invalid confirmation code, or other unexpected errors.
        /// </summary>
        /// <param name="memberId">The ID of the member to activate.</param>
        /// <param name="confirmCode">The confirmation code provided by the user.</param>
        /// <returns>A message indicating successful activation.</returns>
        /// <exception cref="MemberServiceException">Thrown if error occured in MemberService.</exception>
        /// <exception cref="Exception">Thrown for any other unexpected errors during activation.</exception>
        public async Task<string> ActivateRegistration(string memberId, string confirmCode)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                // Find the member by ID, Throw an exception if member not found
                MemberDTO dto = (await _unitOfWork.Members.GetByIdAsync(memberId) ?? throw new MemberServiceException("Member doesn't exist!")).ToDTO<MemberDTO>();

                // Validate the confirmation code
                if (string.Compare(dto.ConfirmCode, confirmCode) != 0) throw new MemberServiceException("Wrong confirm code!");

                await _unitOfWork.Members.UpdateAsync(dto.ToEntity<Member>());

                await _unitOfWork.CommitTransactionAsync();
                return "Activation successful";
            }
            catch (MemberAccessException)
            {
                await _unitOfWork.RollbackTransactionAsync(); // Roll back the transaction on error
                throw; // Re-throw the exception for handling in the controller
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync(); // Roll back the transaction on error
                throw; // Re-throw the exception for handling in the controller
            }
        }

        public async Task<string> ValidateUser(LoginDTO dto)
        {
            var member = await _unitOfWork.Members.GetByAccountAsync(dto.Account);
            if (member == null || !ValidatePassword(member.ToDTO<MemberDTO>(), dto.Password))
            {
                return string.Empty;
            }

            return member.IsConfirmed ? Role.Member : Role.Guest;
        }

        private static bool ValidatePassword(MemberDTO member, string password)
        {
            return HashUtility.ToSHA256(password, member.Salt) == member.EncryptedPassword;
        }

        public async Task<IEnumerable<MemberDTO>> ListMembers()
        {
            return (await _unitOfWork.Members.GetAllAsync()).Select(x => x.ToDTO<MemberDTO>());
        }
        public async Task<MemberDTO> GetMemberInfo(string account) 
        {
            var entity = await _unitOfWork.Members.GetByAccountAsync(account) ?? throw new MemberServiceException("Member doesn't exist!");
            return entity.ToDTO<MemberDTO>();
        }

        public async Task<bool> CheckAccountExist(string account)
        {
            var member = await _unitOfWork.Members.GetByAccountAsync(account);

            return member != null;
        }

        public async Task<bool> CheckNameExist(string name)
        {
            var member = await _unitOfWork.Members.GetByNameAsync(name);

            return member != null;
        }

        public async Task<bool> CheckEmailExist(string email)
        {
            var member = await _unitOfWork.Members.GetByEmailAsync(email);

            return member != null;
        }
    }

    public class MemberServiceException(string message) : Exception(message)
    {
    }
}
