using BoardGame.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace BoardGame.Repositories.Interfaces
{
    public interface IAdminRepository : IRepository
    {
        public IMongoClient GetMongoClient();
        public DbContext GetContext();
        public bool CheckAccountExist(string account);
        public Task<AdminDTO?> SearchByAccount(string account);
        public void AddAdmin(AdminCreateDTO dto);
    }
}
