using BoardGame.Models.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace BoardGame.Services.Interfaces
{
    public interface IMemberService
    {
        public IEnumerable<MemberDTO> ListMembers();

        public Task<MemberDTO> GetMemberInfo(string account);

        public Task<string> Register(RegisterDTO dto, string confirmationUrlTemplate);

        public string ActivateRegistration(string memberId, string confirmCode);

        public Task<string> ValidateUser(LoginDTO dto);

        public Task<string> GenerateToken(string account);
    }
}
