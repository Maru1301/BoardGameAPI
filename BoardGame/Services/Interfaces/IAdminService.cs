using BoardGame.Models.DTOs;

namespace BoardGame.Services.Interfaces
{
    public interface IAdminService
    {
        public Task<string> ValidateUser(LoginDTO dto);
        public string AddAdmin(AdminCreateDTO dto);
    }
}
