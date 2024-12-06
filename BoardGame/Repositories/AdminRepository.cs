using BoardGame.Models.EFModels;
using BoardGame.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BoardGame.Repositories
{
    public class AdminRepository(AppDbContext dbContext) : IAdminRepository
    {
        private readonly AppDbContext _db = dbContext;

        public async Task<Admin?> GetByIdAsync(ObjectId id)
        {
            return await _db.Admins.FindAsync(id);
        }

        public async Task<IEnumerable<Admin>> GetAllAsync()
        {
            return await _db.Admins.ToListAsync();
        }

        public async Task<bool> AddAsync(Admin entity)
        {
            await _db.Admins.AddAsync(entity);

            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Admin entity)
        {
            _db.Admins.Update(entity);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Admin entity)
        {
            _db.Admins.Remove(entity);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<Admin?> GetByAccountAsync(string account)
        {
            var admin = await _db.Admins.FirstOrDefaultAsync(admin => admin.Account == account);

            return admin;
        }
    }
}
