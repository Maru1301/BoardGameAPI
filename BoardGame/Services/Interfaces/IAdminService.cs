using BoardGame.Models.DTO;
using FluentResults;

namespace BoardGame.Services.Interfaces
{
    public interface IAdminService
    {
        Task<Result<string>> ValidateUser(LoginRequestDTO dto);
        Task<string> AddAdmin(AdminCreateDTO dto);
    }
}
