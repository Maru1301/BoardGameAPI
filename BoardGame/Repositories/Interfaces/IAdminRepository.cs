using BoardGame.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BoardGame.Repositories.Interfaces
{
    public interface IAdminRepository : IRepository
    {
        public bool CheckAccountExist(string account);
        public Task<AdminDTO?> SearchByAccountAsync(string account);
        public Task AddAdminAsync(AdminCreateDTO dto);
    }
}
