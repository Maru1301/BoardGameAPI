using BoardGame.Models.DTOs;

namespace BoardGame.Repositories.Interfaces
{
    public interface IMemberRepository
    {
        public IEnumerable<MemberDTO> GetAll();
        public Task<MemberDTO?> SearchByAccount(string account);
        public Task<MemberDTO?> SearchById(string id);
        public Task<MemberDTO?> SearchByName(string name);
        public Task<MemberDTO?> SearchByEmail(string email);
        public Task Register(RegisterDTO dto);
        public Task ActivateRegistration(string memberId);
    }
}
