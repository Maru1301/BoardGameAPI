using BoardGame.Models.DTOs;
using MongoDB.Bson;

namespace BoardGame.Services.Interfaces
{
    public interface IMemberService
    {
        public Task<IEnumerable<MemberDTO>> ListMembers();

        public Task<MemberDTO> GetMemberInfo(ObjectId memberId);

        public Task<string> Register(RegisterDTO dto, string domainName);

        public Task<string> ResendConfirmationCode(ObjectId memberId, string domainName);

        public Task<string> EditMemberInfo(EditDTO dto);

        public Task<string> ResetPassword(ResetPasswordDTO dto);

        public Task<string> ValidateEmail(ObjectId memberId, string confirmCode);

        public Task<string> ValidateUser(LoginDTO dto);

        public Task<bool> IsAccountAvailableAsync(string account);

        public Task<bool> IsNameAvailableAsync(string nickName);

        public Task<bool> IsEmailAvailableAsync(string email);

        Task<bool> IsEmailAvailableAsync(string email, ObjectId memberId);
    }
}
