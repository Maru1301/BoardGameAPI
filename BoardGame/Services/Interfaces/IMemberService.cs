using BoardGame.Models.DTOs;

namespace BoardGame.Services.Interfaces
{
    public interface IMemberService
    {
        public IEnumerable<MemberDTO> ListMembers();

        public Task<MemberDTO> GetMemberInfo(string account);

        public Task<string> Register(RegisterDTO dto, string confirmationUrlTemplate);

        public Task<string> ActivateRegistration(string memberId, string confirmCode);

        public Task<string> ValidateUser(LoginDTO dto);
    }
}
