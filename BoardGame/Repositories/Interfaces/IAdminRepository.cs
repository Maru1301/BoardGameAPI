using BoardGame.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BoardGame.Repositories.Interfaces
{
    public interface IAdminRepository : IRepository
    {
        public DbContext GetContext();

        public bool CheckAccountExist(string account);

        public Task<AdminDTO?> SearchByAccount(string account);

        public void AddAdmin(AdminCreateDTO dto);
    }
}
