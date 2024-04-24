using BoardGame.Models.DTOs;

namespace BoardGame.Services.Interfaces
{
    public interface IAdminService
    {
        public Task<string> ValidateUser(LoginDTO dto);
        public void AddAdmin(AdminCreateDTO dto);
    }
}
