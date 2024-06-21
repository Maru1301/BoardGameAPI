using BoardGame.Infrastractures;
using BoardGame.Models.EFModels;
using BoardGame.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace BoardGame.Repositories
{
    public class GameRepository(AppDbContext dbContext) : IGameRepository
    {
        public async Task<Game?> GetByIdAsync(ObjectId id)
        {
            return await _db.Games.FindAsync(id);
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _db.Games.ToListWithNoLockAsync();
        }

        private readonly AppDbContext _db = dbContext;
        public async Task<ObjectId> AddAsync(Game entity)
        {
            var entry = await _db.Games.AddAsync(entity);
            await _db.SaveChangesAsync();

            return entry.Entity.Id;
        }

        public async Task UpdateAsync(Game entity)
        {
            _db.Games.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(ObjectId id)
        {
            var entity = await _db.Games.FindAsync(id);
            if (entity != null)
            {
                _db.Games.Remove(entity);
                await _db.SaveChangesAsync();
            }
        }
    }
}
