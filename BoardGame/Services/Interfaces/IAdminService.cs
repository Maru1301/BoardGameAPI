using BoardGame.Models.DTO;

namespace BoardGame.Services.Interfaces
{
    public interface IAdminService
    {
        public Task<string> ValidateUser(LoginRequestDTO dto);
        public Task<string> AddAdmin(AdminCreateDTO dto);
    }
}
