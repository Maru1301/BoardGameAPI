using BoardGame.Models.DTOs;
using MongoDB.Bson;

namespace BoardGame.Repositories
{
    public interface IMemberRepository
    {
        public bool CheckAccountExist(string account);
        public bool CheckNameExist(string nickName);
        public bool CheckEmailExist(string email);
        public void Register(MemberRegisterDTO dto);
        public void ActivateRegistration(string memberId);
        public MemberDTO? SearchByAccount(string account);
        public MemberDTO? SearchById(string id);
    }
}
