using BoardGame.Models.DTOs;
using BoardGame.Infrastractures;
using Utilities;
using BoardGame.Services.Interfaces;
using BoardGame.Repositories.Interfaces;

namespace BoardGame.Services
{
    public class MemberService : IService, IMemberService
    {
        private readonly IMemberRepository _repository;
        private readonly IConfiguration _configuration;

        public MemberService(IMemberRepository repo, IConfiguration configuration)
        {
            _repository = repo;
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
        public async Task<string> Register(RegisterDTO dto, string confirmationUrlTemplate)
        {
            using var session = _repository.GetMongoClient().StartSession();
            try
            {
                if (_repository.CheckAccountExist(dto.Account))
                {
                    throw new Exception("Account already exists");
                }
                if (_repository.CheckNameExist(dto.Name))
                {
                    throw new Exception("Name already exists");
                }
                if (_repository.CheckEmailExist(dto.Email))
                {
                    throw new Exception("Email already exists");
                }

                //create a new confirm code
                dto.ConfirmCode = Guid.NewGuid().ToString("N");

                _repository.Register(dto);

                MemberDTO entity = await _repository.SearchByAccount(dto.Account) ?? throw new Exception("Member doesn't exist!");

                // Generate confirmation URL
                string url = $"{confirmationUrlTemplate}?memberId={entity.Id}&confirmCode={dto.ConfirmCode}";

                // Send confirmation email
                new EmailHelper(_configuration).SendConfirmRegisterEmail(url, dto.Name!, dto.Email!);

                session.CommitTransaction();
                return "Registration successful! Confirmation email sent!";
            }
            catch (Exception)
            {
                session.AbortTransaction(); // Roll back the transaction on error
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
            using var session = _repository.GetMongoClient().StartSession();
            try
            {
                MemberDTO entity = _repository.SearchById(memberId) ?? throw new Exception("Member doesn't exist!");

                if (string.Compare(entity.ConfirmCode, confirmCode) != 0) return "Wrong confirm code!";

                _repository.ActivateRegistration(memberId);
                session.CommitTransaction();
                return "Activation successful";
            }
            catch (Exception)
            {
                session.AbortTransaction(); // Roll back the transaction on error
                throw; // Re-throw the exception for handling in the controller
            }
        }

        public async Task<string> ValidateUser(LoginDTO dto)
        {
            var member = await _repository.SearchByAccount(dto.Account);
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

        public  IEnumerable<MemberDTO> ListMembers()
        {
            return _repository.GetAll();
        }
        public async Task<MemberDTO> GetMemberInfo(string account) 
        {
            var dto = await _repository.SearchByAccount(account);
            return dto ?? throw new MemberServiceException("Member doesn't exist!");
        }
    }

    public class MemberServiceException : Exception
    {
        public MemberServiceException(string message) : base(message){}
    }
}
