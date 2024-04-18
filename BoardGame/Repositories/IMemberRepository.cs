using BoardGame.Models.DTOs;

namespace BoardGame.Repositories
{
    public interface IMemberRepository
    {
        public bool CheckAccountExist(string account);
        public bool CheckNameExist(string nickName);
        public bool CheckEmailExist(string email);
        public void Register(MemberRegisterDTO dto);
        public MemberDTO SearchByAccount(string account);
    }
}
