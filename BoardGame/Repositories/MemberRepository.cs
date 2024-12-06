using BoardGame.Infrastractures;
using BoardGame.Models.EFModels;
using BoardGame.Repositories.Interfaces;
using FluentResults;
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
            return await _db.Members.ToListWithNoLockAsync();
        }

        public async Task<bool> AddAsync(Member entity)
        {
            await _db.Members.AddAsync(entity);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Member entity)
        {
            _db.Members.Update(entity);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Member entity)
        {
            _db.Members.Remove(entity);
            return await _db.SaveChangesAsync() > 0;
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