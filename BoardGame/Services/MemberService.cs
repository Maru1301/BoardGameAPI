using BoardGame.Models.DTO;
using BoardGame.Infrastractures;
using BoardGame.Services.Interfaces;
using BoardGame.Models.EFModels;
using MongoDB.Bson;
using System.Data;
using Utility;
using FluentResults;
using System.Net.Mail;

namespace BoardGame.Services
{
    public class MemberService(IUnitOfWork unitOfWork, IConfig config, JWTHelper jwt) : IService, IMemberService
    {
        /// <summary>
        /// Registers a new user using the provided registration data.
        /// Ensures account, name, and email are unique before proceeding.
        /// If the registration is successful, generates a confirmation URL 
        /// and sends a verification email to the user's email address.
        /// </summary>
        /// <param name="dto">A `RegisterRequestDTO` object containing the user's registration details.</param>
        /// <returns>A `Result<string>` indicating the success or failure of the registration process.</returns>
        /// <exception cref="Exception">Thrown when an unexpected error occurs during registration.</exception>
        public async Task<Result<string>> Register(RegisterRequestDTO dto)
        {
            if (!await IsAccountAvailableAsync(dto.Account)) 
            { 
                return Result.Fail(ErrorCode.AccountExist); 
            }
            if (!await IsNameAvailableAsync(dto.Name)) 
            {
                return Result.Fail(ErrorCode.NameExist); 
            }
            if (!await IsEmailAvailableAsync(dto.Email)) 
            {
                return Result.Fail(ErrorCode.EmailExist); 
            }

            //create a new confirm code
            dto.ConfirmCode = Guid.NewGuid().ToString("N");

            try
            {
                await unitOfWork.BeginTransactionAsync();
                var addResult = await unitOfWork.Members.AddAsync(dto.To<Member>());
                await unitOfWork.CommitTransactionAsync();
            }
            catch (Exception)
            {
                await unitOfWork.RollbackTransactionAsync();
                throw;
            }

            var member = (await unitOfWork.Members.GetByAccountAsync(dto.Account));
                
            if(member == null)
            {
                return Result.Fail(new DataNotFoundError());
            }

            // Define a template for the confirmation email URL.
            string url = $"{config.DomainName}{config.ApiUrl.ValidateEmail}?memberId={member.Id}&confirmCode={member.ConfirmCode}";

            var mailResult = SendConfirmationEmail(url, member.Name!, member.Email!);

            await unitOfWork.CommitTransactionAsync();
            return mailResult.WithSuccess("Registration successful!");
        }

        /// <summary>
        /// Sends a confirmation email to a newly registered user.
        /// </summary>
        /// <param name="confirmationUrl">The URL for the user to click to confirm their registration.</param>
        /// <param name="name">The name of the registered user.</param>
        /// <param name="emailAddress">The email address of the registered user.</param>
        private Result<string> SendConfirmationEmail(string confirmationUrl, string name, string to)
        {
            var from = config.EmailConfig.Account;
            if (string.IsNullOrEmpty(from))
            {
                return Result.Fail("Fail to send confirmation mail");
            }
            var pw = config.EmailConfig.Password;
            if (string.IsNullOrEmpty(pw))
            {
                return Result.Fail("Fail to send confirmation mail");
            }

            string body = $@"Hi {name},

						<br />
                        Please click on this link [<a href='{confirmationUrl}' target='_blank'>Verify Email</a>] to activate your account.
                        <br />
                        If you did not request this email, please ignore it. Thank you!

                        <br />
                        Sincerely,
                        The {config.AppName} Team";

            var mailMessage = new MailMessage(from, to, "[New Member Confirmation Email]", body)
            {
                IsBodyHtml = true
            };

            EmailUtility.SendEmailViaGmailSmtp(mailMessage, from, pw);

            return Result.Ok("Confirmation mail sent!");
        }

        public async Task<Result<string>> ResendConfirmationCode(ObjectId memberId)
        {
            var member = await unitOfWork.Members.GetByIdAsync(memberId);

            if(member == null)
            {
                return Result.Fail(new DataNotFoundError());
            }

            //create a new confirm code
            var confirmCode = Guid.NewGuid().ToString("N");
            member.ConfirmCode = confirmCode;

            try
            {
                await unitOfWork.BeginTransactionAsync();
                await unitOfWork.Members.UpdateAsync(member);
                await unitOfWork.CommitTransactionAsync();
            }
            catch (Exception)
            {
                await unitOfWork.RollbackTransactionAsync();
                throw;
            }

            // Define a template for the confirmation email URL.
            string url = $"{config.DomainName}{config.ApiUrl.ValidateEmail}?memberId={member.Id}&confirmCode={confirmCode}";

            return SendConfirmationEmail(url, member.Name!, member.Email!);
        }

        /// <summary>
        /// Edits member information.
        /// Throws specific exceptions for duplicate email.
        /// If email is modified, set IsConfirmed to false to let member verified again.
        /// </summary>
        /// <param name="dto">The EditDTO containing member ID, name, and new email address.</param>
        /// <returns>A string indicating successful edit ("Edition successful")</returns>
        /// <exception cref="Exception">Thrown for any other unexpected errors during update.</exception>
        public async Task<Result<string>> EditMemberInfo(EditDTO dto)
        {
            if (await IsEmailAvailableAsync(dto.Email, dto.Id) == false) 
            {
                return Result.Fail(ErrorCode.EmailExist); 
            }

            var entity = await unitOfWork.Members.GetByIdAsync(dto.Id);
            if(entity == null)
            {
                return Result.Fail(new DataNotFoundError());
            }

            // Update member information
            entity.Name = dto.Name;
            if(!entity.Email.Equals(dto.Email))
            {
                entity.IsConfirmed = false;
            }
            entity.Email = dto.Email;

            try
            {
                await unitOfWork.BeginTransactionAsync();
                await unitOfWork.Members.UpdateAsync(entity);
                await unitOfWork.CommitTransactionAsync();
            }
            catch (Exception)
            {
                await unitOfWork.RollbackTransactionAsync();
                throw;
            }

            return Result.Ok("Edition successful");
        }

        /// <summary>
        /// Resets a member's password.
        /// </summary>
        /// <param name="dto">A ResetPasswordDTO object containing the member's ID, old password, new password, and salt.</param>
        /// <returns>A string indicating success ("Reset successful").</returns>
        /// <exception cref="Exception">Thrown for any other unexpected errors during update.</exception>
        public async Task<Result<string>> ResetPassword(ResetPasswordDTO dto)
        {
            var entity = await unitOfWork.Members.GetByIdAsync(dto.Id);
            if(entity == null)
            {
                return Result.Fail(new DataNotFoundError());
            }

            // Validate old password matches the hashed and salted password in the database
            var oldEncryptedPassword = HashUtility.ToSHA256(dto.OldPassword, entity.Salt);
            if (!entity.EncryptedPassword.Equals(oldEncryptedPassword))
            {
                return Result.Fail("Old password not match.");
            }

            // Update member entity with new password and salt
            entity.Salt = dto.Salt;
            entity.EncryptedPassword = dto.EncryptedPassword;

            try
            {
                await unitOfWork.BeginTransactionAsync();
                await unitOfWork.Members.UpdateAsync(entity);
                await unitOfWork.CommitTransactionAsync();
            }
            catch (Exception)
            {
                await unitOfWork.RollbackTransactionAsync();
                throw;
            }

            return Result.Ok("Reset successful");
        }

        /// <summary>
        /// Validates a member's email address by verifying their ID and confirmation code.
        /// Throws specific exceptions for member not found, invalid confirmation code, or other unexpected errors.
        /// </summary>
        /// <param name="memberId">The ID of the member to activate.</param>
        /// <param name="confirmCode">The confirmation code provided by the user.</param>
        /// <returns>A message indicating successful email validation ("Validation successful").</returns>
        /// <exception cref="Exception">Thrown for any other unexpected errors during validation.</exception>
        public async Task<Result<string>> ValidateEmail(string memberId, string confirmCode)
        {
            var entity = await unitOfWork.Members.GetByIdAsync(new ObjectId(memberId));
            if (entity == null)
            {
                return Result.Fail(new DataNotFoundError());
            }

            // Validate the confirmation code
            if (string.Compare(entity.ConfirmCode, confirmCode) != 0) 
            {
                return Result.Fail(ErrorCode.WrongConfirmationCode);
            }

            entity.IsConfirmed = true;

            try
            {
                await unitOfWork.BeginTransactionAsync();
                await unitOfWork.Members.UpdateAsync(entity);
                await unitOfWork.CommitTransactionAsync();
            }
            catch (Exception)
            {
                await unitOfWork.RollbackTransactionAsync();
                throw;
            }

            return Result.Ok("Validation successful");
        }

        public async Task<Result<string>> ValidateUser(LoginRequestDTO dto)
        {
            var member = await unitOfWork.Members.GetByAccountAsync(dto.Account);
            if(member == null)
            {
                return Result.Fail(ErrorCode.InvalidAccountOrPassword);
            }

            if(!ValidatePassword(member.To<MemberDTO>(), dto.Password))
            {
                return Result.Fail(ErrorCode.InvalidAccountOrPassword);
            }

            var result = jwt.GenerateToken(member.Id, member.Account, member.IsConfirmed ? Role.Member : Role.Guest);

            return result;
        }

        private static bool ValidatePassword(MemberDTO member, string password)
        {
            return HashUtility.ToSHA256(password, member.Salt) == member.EncryptedPassword;
        }

        public async Task<IEnumerable<MemberDTO>> ListMembers()
        {
            var members = await unitOfWork.Members.GetAllAsync();

            return members.Select(x => x.To<MemberDTO>());
        }

        public async Task<Result<MemberDTO>> GetMemberInfo(ObjectId id) 
        {
            var member = await unitOfWork.Members.GetByIdAsync(id);
            if(member == null)
            {
                return Result.Fail(new DataNotFoundError());
            }
            
            return member.To<MemberDTO>();
        }

        private async Task<bool> IsAccountAvailableAsync(string account)
        {
            var member = await unitOfWork.Members.GetByAccountAsync(account);

            return member == null;
        }

        private async Task<bool> IsNameAvailableAsync(string name)
        {
            var member = await unitOfWork.Members.GetByNameAsync(name);

            return member == null;
        }

        /// <summary>
        /// Checks if the provided email address is available for use by a member.
        /// </summary>
        /// <param name="email">The email address to check for availability.</param>
        /// <returns>True if the email address is not in use by any other member, false otherwise.</returns>
        private async Task<bool> IsEmailAvailableAsync(string email)
        {
            var member = await unitOfWork.Members.GetByEmailAsync(email);

            return member == null;
        }

        /// <summary>
        /// Checks if the provided email address is available for use by a member.
        /// This method excludes the member identified by the memberId parameter from the check.
        /// </summary>
        /// <param name="email">The email address to check for availability.</param>
        /// <param name="memberId">The ID of the member to exclude from the check.</param>
        /// <returns>True if the email address is not in use by any other member, false otherwise.</returns>
        private async Task<bool> IsEmailAvailableAsync(string email, ObjectId memberId)
        {
            var member = await unitOfWork.Members.GetByEmailAsync(email);

            return member == null || (member != null && member.Id == memberId);
        }

        public async Task<Result<bool>> DeleteMember(string account)
        {
            var member = await unitOfWork.Members.GetByAccountAsync(account);
            if (member == null)
            {
                return Result.Fail(new DataNotFoundError());
            }

            await unitOfWork.Members.DeleteAsync(member);

            return true;
        }
    }
}
