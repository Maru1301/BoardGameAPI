using BoardGame.Models.EFModels;
using BoardGame.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BoardGame.Repositories
{
    public class MemberRepository(AppDbContext dbContext) : IMemberRepository
    {
        private readonly AppDbContext _db = dbContext;

        public async Task<Member?> GetByIdAsync(ObjectId id)
        {
            return await _db.Members.FindAsync(id);
        }

        public async Task<IEnumerable<Member>> GetAllAsync()
        {
            return await _db.Members.ToListAsync();
        }

        public async Task<ObjectId> AddAsync(Member entity)
        {
            var entry = await _db.Members.AddAsync(entity);
            await _db.SaveChangesAsync();

            return entry.Entity.Id;
        }

        public async Task UpdateAsync(Member entity)
        {
            _db.Members.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(ObjectId id)
        {
            var user = await _db.Members.FindAsync(id);
            if (user != null)
            {
                _db.Members.Remove(user);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<Member?> GetByAccountAsync(string account)
        {
            return await _db.Members.FirstOrDefaultAsync(member => member.Account == account);
        }

        public async Task<Member?> GetByNameAsync(string name)
        {
            return await _db.Members.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<Member?> GetByEmailAsync(string email)
        {
            return await _db.Members.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}