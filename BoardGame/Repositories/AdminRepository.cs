using BoardGame.Infrastractures;
using BoardGame.Models.DTOs;
using BoardGame.Models.EFModels;
using BoardGame.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace BoardGame.Repositories
{
    public class AdminRepository : IRepository, IAdminRepository
    {
        private readonly AppDbContext _db;

        public AdminRepository(IMongoClient client)
        {
            _db = AppDbContext.Create(client.GetDatabase("BoardGameDB"));
        }

        public DbContext GetMongoClient()
        {
            return _db;
        }

        public async void AddAdmin(AdminCreateDTO dto)
        {
            _db.Admins.Add(dto.ToEntity<Admin>());
            await _db.SaveChangesAsync();
        }

        public bool CheckAccountExist(string account)
        {
            var entity = _db.Admins.FirstOrDefault(m => m.Account == account);

            return entity != null;
        }

        public DbContext GetContext()
        {
            return _db;
        }

        public async Task<AdminDTO?> SearchByAccount(string account)
        {
            var admin = await _db.Admins.FirstOrDefaultAsync(admin => admin.Account == account);

            return admin?.ToDTO<AdminDTO>();
        }
    }
}
