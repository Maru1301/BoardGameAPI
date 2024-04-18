using BoardGame.Models.DTOs;

namespace BoardGame.Services
{
    public interface IMemberService
    {
        public (bool Success, string Message) Register(MemberRegisterDTO dto, string confirmationUrlTemplate);
    }
}
