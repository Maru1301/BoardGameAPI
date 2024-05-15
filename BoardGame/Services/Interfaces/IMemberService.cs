using BoardGame.Models.DTOs;
using MongoDB.Bson;

namespace BoardGame.Services.Interfaces
{
    public interface IMemberService
    {
        public Task<IEnumerable<MemberDTO>> ListMembers();

        public Task<MemberDTO> GetMemberInfo(ObjectId memberId);

        public Task<string> Register(RegisterDTO dto, string confirmationUrlTemplate);

        public Task<string> EditMemberInfo(EditDTO dto);

        public Task<string> ResetPassword(ResetPasswordDTO dto);

        public Task<string> ActivateRegistration(ObjectId memberId, string confirmCode);

        public Task<(ObjectId Id, string Account)> ValidateUser(LoginDTO dto);

        public Task<bool> CheckAccountExist(string account);

        public Task<bool> CheckNameExist(string nickName);

        public Task<bool> CheckEmailExist(string email);
    }
}
