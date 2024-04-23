using BoardGame.Models.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace BoardGame.Services
{
    public interface IMemberService
    {
        public IEnumerable<MemberDTO> ListMembers();

        public Task<string> Register(RegisterDTO dto, string confirmationUrlTemplate);

        public string ActivateRegistration(string memberId, string confirmCode);

        public Task<bool> ValidateUser(LoginDTO dto);

        public Task<string> GenerateToken(string account);
    }
}
