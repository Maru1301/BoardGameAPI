using BoardGame.Models.DTO;
using FluentResults;
using MongoDB.Bson;

namespace BoardGame.Services.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberDTO>> ListMembers();
        Task<Result<MemberDTO>> GetMemberInfo(ObjectId memberId);

        /// <summary>
        /// Registers a new user using the provided registration data.
        /// Ensures account, name, and email are unique before proceeding.
        /// If the registration is successful, generates a confirmation URL 
        /// and sends a verification email to the user's email address.
        /// </summary>
        /// <param name="dto">A `RegisterRequestDTO` object containing the user's registration details.</param>
        /// <returns>A `Result<string>` indicating the success or failure of the registration process.</returns>
        /// <exception cref="Exception">Thrown when an unexpected error occurs during registration.</exception>
        Task<Result<string>> Register(RegisterRequestDTO dto);
        Task<Result<string>> ResendConfirmationCode(ObjectId memberId);
        Task<Result<string>> EditMemberInfo(EditDTO dto);
        Task<Result<string>> ResetPassword(ResetPasswordDTO dto);
        Task<Result<string>> ValidateEmail(string memberId, string confirmCode);
        Task<Result<string>> ValidateUser(LoginRequestDTO dto);
        Task<Result<bool>> DeleteMember(string account);
    }
}
