using BoardGame.Models.DTOs;
using BoardGame.Infrastractures;
using Utilities;
using BoardGame.Services.Interfaces;
using BoardGame.Models.EFModels;
using MongoDB.Bson;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Data;

namespace BoardGame.Services
{
    public class MemberService(IUnitOfWork unitOfWork, IConfiguration configuration, ICacheService cacheService, JWTHelper jwt) : IService, IMemberService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IConfiguration _configuration = configuration;
        private readonly ICacheService _cacheService = cacheService;
        private readonly JWTHelper _jwt = jwt;

        /// <summary>
        /// Register a new user based on the provided information.
        /// Throws specific exceptions for duplicate account, name, or email.
        /// On successful registration, a confirmation email is sent with a generated confirmation URL
        /// for account verification.
        /// </summary>
        /// <param name="dto">The `RegisterDTO` object containing user registration information.</param>
        /// <param name="confirmationUrlTemplate">The template string used to generate the confirmation URL.</param>
        /// <returns>A message indicating successful registration.</returns>
        /// <exception cref="MemberServiceException">Thrown if error occured in MemberService.</exception>
        /// <exception cref="Exception">Thrown for any other unexpected errors during registration.</exception>
        public async Task<string> Register(RegisterDTO dto, string domainName)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                if (!await IsAccountAvailableAsync(dto.Account)) throw new MemberServiceException(ErrorCode.AccountExist);
                if (!await IsNameAvailableAsync(dto.Name)) throw new MemberServiceException(ErrorCode.NameExist);
                if (!await IsEmailAvailableAsync(dto.Email)) throw new MemberServiceException(ErrorCode.EmailExist);

                //create a new confirm code
                dto.ConfirmCode = Guid.NewGuid().ToString("N");

                await _unitOfWork.Members.AddAsync(dto.To<Member>());
                await _unitOfWork.CommitTransactionAsync();

                var entity = (await _unitOfWork.Members.GetByAccountAsync(dto.Account) ?? throw new MemberServiceException(ErrorCode.MemberNotExist));

                // Define a template for the confirmation email URL.
                string confirmationUrlTemplate = $"{domainName}/Member/ActivateRegistration";

                SendConfirmationCode(entity, confirmationUrlTemplate);

                await _unitOfWork.CommitTransactionAsync();
                return "Registration successful! Confirmation email sent!";
            }
            catch (MemberServiceException)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public void SendConfirmationCode(Member entity, string confirmationUrlTemplate)
        {
            // Generate confirmation URL
            string url = $"{confirmationUrlTemplate}?memberId={entity.Id}&confirmCode={entity.ConfirmCode}";

            new EmailHelper(_configuration).SendConfirmationEmail(url, entity.Name!, entity.Email!);
        }

        public async Task<string> ResendConfirmationCode(ObjectId memberId, string domainName)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var entity = (await _unitOfWork.Members.GetByIdAsync(memberId) ?? throw new MemberServiceException(ErrorCode.MemberNotExist));

                // Define a template for the confirmation email URL.
                string confirmationUrlTemplate = $"{domainName}/Member/ValidateEmail";

                SendConfirmationCode(entity, confirmationUrlTemplate);

                return "success";
            }
            catch (MemberServiceException)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        /// <summary>
        /// Edits member information.
        /// Throws specific exceptions for duplicate email.
        /// If email is modified, set IsConfirmed to false to let member verified again.
        /// </summary>
        /// <param name="dto">The EditDTO containing member ID, name, and new email address.</param>
        /// <returns>A string indicating successful edit ("Edition successful")</returns>
        /// /// <exception cref="MemberServiceException">Thrown if member not found or old password doesn't matched the password in the database.</exception>
        /// <exception cref="Exception">Thrown for any other unexpected errors during update.</exception>
        public async Task<string> EditMemberInfo(EditDTO dto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                if (await IsEmailAvailableAsync(dto.Email, dto.Id)) throw new MemberServiceException(ErrorCode.EmailExist);

                var entity = (await _unitOfWork.Members.GetByIdAsync(dto.Id) ?? throw new MemberServiceException(ErrorCode.MemberNotExist));

                // Update member information
                entity.Name = dto.Name;
                if(!entity.Email.Equals(dto.Email))
                {
                    entity.IsConfirmed = false;
                }
                entity.Email = dto.Email;

                await _unitOfWork.Members.UpdateAsync(entity);

                await _unitOfWork.CommitTransactionAsync();
                return "Edition successful";
            }
            catch (MemberServiceException)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        /// <summary>
        /// Resets a member's password.
        /// </summary>
        /// <param name="dto">A ResetPasswordDTO object containing the member's ID, old password, new password, and salt.</param>
        /// <returns>A string indicating success ("Reset successful").</returns>
        /// <exception cref="MemberServiceException">Thrown if member not found or old password doesn't matched the password in the database.</exception>
        /// <exception cref="Exception">Thrown for any other unexpected errors during update.</exception>
        public async Task<string> ResetPassword(ResetPasswordDTO dto)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var entity = await _unitOfWork.Members.GetByIdAsync(dto.Id) ?? throw new MemberServiceException(ErrorCode.MemberNotExist);

                // Validate old password matches the hashed and salted password in the database
                var oldEncryptedPassword = HashUtility.ToSHA256(dto.OldPassword, entity.Salt);
                if (!entity.EncryptedPassword.Equals(oldEncryptedPassword))
                {
                    throw new MemberServiceException("Old password confirmation failed");
                }

                // Update member entity with new password and salt
                entity.Salt = dto.Salt;
                entity.EncryptedPassword = dto.EncryptedPassword;

                await _unitOfWork.Members.UpdateAsync(entity);

                await _unitOfWork.CommitTransactionAsync();
                return "Reset successful";
            }
            catch (MemberServiceException)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        /// <summary>
        /// Validates a member's email address by verifying their ID and confirmation code.
        /// Throws specific exceptions for member not found, invalid confirmation code, or other unexpected errors.
        /// </summary>
        /// <param name="memberId">The ID of the member to activate.</param>
        /// <param name="confirmCode">The confirmation code provided by the user.</param>
        /// <returns>A message indicating successful email validation ("Validation successful").</returns>
        /// <exception cref="MemberServiceException">Thrown if the member is not found or there's an error within the MemberService logic.</exception>
        /// <exception cref="Exception">Thrown for any other unexpected errors during validation.</exception>
        public async Task<string> ValidateEmail(ObjectId memberId, string confirmCode)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                MemberDTO dto = (await _unitOfWork.Members.GetByIdAsync(memberId) ?? throw new MemberServiceException(ErrorCode.MemberNotExist)).To<MemberDTO>();

                // Validate the confirmation code
                if (string.Compare(dto.ConfirmCode, confirmCode) != 0) throw new MemberServiceException(ErrorCode.WrongConfirmationCode);

                await _unitOfWork.Members.UpdateAsync(dto.To<Member>());

                await _unitOfWork.CommitTransactionAsync();
                return "Validation successful";
            }
            catch (MemberServiceException)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<string> ValidateUser(LoginDTO dto)
        {
            var member = await _unitOfWork.Members.GetByAccountAsync(dto.Account) ?? throw new MemberServiceException(ErrorCode.InvalidAccountOrPassword);
            
            if(!ValidatePassword(member.To<MemberDTO>(), dto.Password)) throw new MemberServiceException(ErrorCode.InvalidAccountOrPassword);

            var token = _jwt.GenerateToken(member.Id, member.Account, member.IsConfirmed ? Infrastractures.Role.Member : Infrastractures.Role.Guest);

            return token;
        }

        private static bool ValidatePassword(MemberDTO member, string password)
        {
            return HashUtility.ToSHA256(password, member.Salt) == member.EncryptedPassword;
        }

        public async Task<IEnumerable<MemberDTO>> ListMembers()
        {
            const string cacheKey = "members";

            // Try to get members from cache
            var cachedMembers = await _cacheService.HashGetAllAsync(cacheKey);

            if (cachedMembers != null && cachedMembers.Length > 0)
            {
                return cachedMembers.Select(cachedMember => JsonConvert.DeserializeObject<MemberDTO>(cachedMember.Value.ToString()))!;
            }

            // If not cached, fetch from database
            var members = (await _unitOfWork.Members.GetAllAsync());

            // Add members to cache with expiration (optional)
            var entries = members.Select(member => new HashEntry(member.Id.ToString(), JsonConvert.SerializeObject(member))).ToArray();
            await _cacheService.HashSetAsync(cacheKey, entries, TimeSpan.FromSeconds(10));

            return members.Select(x => x.To<MemberDTO>());
        }

        public async Task<MemberDTO> GetMemberInfo(ObjectId id) 
        {
            const string cacheKey = "members";

            // Try to get members from cache
            var cachedMember = await _cacheService.HashGetAsync(cacheKey, id.ToString());

            if (cachedMember.HasValue)
            {
                return JsonConvert.DeserializeObject<MemberDTO>(cachedMember.ToString())!;
            }

            var member = await _unitOfWork.Members.GetByIdAsync(id) ?? throw new MemberServiceException(ErrorCode.MemberNotExist);

            // Add members to cache with expiration
            var entries = new HashEntry(member.Id.ToString(), JsonConvert.SerializeObject(member));
            await _cacheService.HashSetAsync(cacheKey, [entries]);

            return member.To<MemberDTO>();
        }

        public async Task<bool> IsAccountAvailableAsync(string account)
        {
            var member = await _unitOfWork.Members.GetByAccountAsync(account);

            return member == null;
        }

        public async Task<bool> IsNameAvailableAsync(string name)
        {
            var member = await _unitOfWork.Members.GetByNameAsync(name);

            return member == null;
        }

        /// <summary>
        /// Checks if the provided email address is available for use by a member.
        /// </summary>
        /// <param name="email">The email address to check for availability.</param>
        /// <returns>True if the email address is not in use by any other member, false otherwise.</returns>
        public async Task<bool> IsEmailAvailableAsync(string email)
        {
            var member = await _unitOfWork.Members.GetByEmailAsync(email);

            return member == null;
        }

        /// <summary>
        /// Checks if the provided email address is available for use by a member.
        /// This method excludes the member identified by the memberId parameter from the check.
        /// </summary>
        /// <param name="email">The email address to check for availability.</param>
        /// <param name="memberId">The ID of the member to exclude from the check.</param>
        /// <returns>True if the email address is not in use by any other member, false otherwise.</returns>
        public async Task<bool> IsEmailAvailableAsync(string email, ObjectId memberId)
        {
            var member = await _unitOfWork.Members.GetByEmailAsync(email);

            return member == null || (member != null && member.Id == memberId);
        }
    }

    /// <summary>
    /// The MemberServiceException is thrown when errors occurred in the service 
    /// </summary>
    public class MemberServiceException(string message) : Exception(message)
    {
    }
}
