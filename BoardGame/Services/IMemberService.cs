using BoardGame.Models.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace BoardGame.Services
{
    public interface IMemberService
    {
        public Task<string> Register(MemberRegisterDTO dto, string confirmationUrlTemplate);

        public string ActivateRegistration(string memberId, string confirmCode);

        public Task<bool> ValidateUser(MemberLoginDTO dto);

        public Task<string> GenerateToken(string account);
    }
}
