using BoardGame.Infrastractures;
using BoardGame.Models.DTOs;
using BoardGame.Models.EFModels;
using BoardGame.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace BoardGame.Repositories
{
    public class AdminRepository(AppDbContext dbContext) : IRepository, IAdminRepository
    {
        private readonly AppDbContext _db = dbContext;

        public async Task AddAdminAsync(AdminCreateDTO dto)
        {
            _db.Admins.Add(dto.ToEntity<Admin>());
            await _db.SaveChangesAsync();
        }

        public bool CheckAccountExist(string account)
        {
            var entity = _db.Admins.FirstOrDefault(m => m.Account == account);

            return entity != null;
        }

        public async Task<AdminDTO?> SearchByAccountAsync(string account)
        {
            var admin = await _db.Admins.FirstOrDefaultAsync(admin => admin.Account == account);

            return admin?.ToDTO<AdminDTO>();
        }
    }
}
