using BoardGame.Models.DTOs;
using BoardGame.Infrastractures;
using Utilities;
using BoardGame.Services.Interfaces;

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
        public async Task<string> Register(RegisterDTO dto, string confirmationUrlTemplate)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                if (CheckAccountExist(dto.Account)) throw new MemberServiceException("Account already exists");
                if (CheckNameExist(dto.Name)) throw new MemberServiceException("Name already exists");
                if (CheckEmailExist(dto.Email)) throw new MemberServiceException("Email already exists");

                //create a new confirm code
                dto.ConfirmCode = Guid.NewGuid().ToString("N");

                await _unitOfWork.Members.Register(dto);

                MemberDTO entity = await _unitOfWork.Members.SearchByAccount(dto.Account) ?? throw new MemberServiceException("Member doesn't exist!");

                // Generate confirmation URL
                string url = $"{confirmationUrlTemplate}?memberId={entity.Id}&confirmCode={dto.ConfirmCode}";

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
                MemberDTO entity = await _unitOfWork.Members.SearchById(memberId) ?? throw new MemberServiceException("Member doesn't exist!");

                // Validate the confirmation code
                if (string.Compare(entity.ConfirmCode, confirmCode) != 0) throw new MemberServiceException("Wrong confirm code!");

                await _unitOfWork.Members.ActivateRegistration(memberId);

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
            var member = await _unitOfWork.Members.SearchByAccount(dto.Account);
            if (member == null || !ValidatePassword(member, dto.Password))
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
            return await _unitOfWork.Members.GetAll();
        }
        public async Task<MemberDTO> GetMemberInfo(string account) 
        {
            var dto = await _unitOfWork.Members.SearchByAccount(account);
            return dto ?? throw new MemberServiceException("Member doesn't exist!");
        }

        public bool CheckAccountExist(string account)
        {
            var member = _unitOfWork.Members.SearchByAccount(account);

            return member != null;
        }

        public bool CheckNameExist(string name)
        {
            var member = _unitOfWork.Members.SearchByName(name);

            return member != null;
        }

        public bool CheckEmailExist(string email)
        {
            var member = _unitOfWork.Members.SearchByEmail(email);

            return member != null;
        }
    }

    public class MemberServiceException(string message) : Exception(message)
    {
    }
}
