using BoardGame.Models.DTOs;
using MongoDB.Bson;

namespace BoardGame.Services.Interfaces
{
    public interface IMemberService
    {
        public Task<IEnumerable<MemberDTO>> ListMembers();

        public Task<MemberDTO> GetMemberInfo(ObjectId memberId);

        public Task<string> Register(RegisterDTO dto, string confirmationUrlTemplate);

        public Task<string> ResendConfirmationCode(ObjectId memberId);

        public Task<string> EditMemberInfo(EditDTO dto);

        public Task<string> ResetPassword(ResetPasswordDTO dto);

        public Task<string> ValidateEmail(ObjectId memberId, string confirmCode);

        public Task<(ObjectId Id, string Account)> ValidateUser(LoginDTO dto);

        public Task<bool> IsAccountAvailableAsync(string account);

        public Task<bool> IsNameAvailableAsync(string nickName);

        public Task<bool> IsEmailAvailableAsync(string email);

        public Task<bool> CheckEmailExist(string email);
    }
}
