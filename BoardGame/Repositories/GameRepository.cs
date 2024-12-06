using BoardGame.Infrastractures;
using BoardGame.Models.EFModels;
using BoardGame.Repositories.Interfaces;
using MongoDB.Bson;

namespace BoardGame.Repositories
{
    public class GameRepository(AppDbContext dbContext) : IGameRepository
    {
        private readonly AppDbContext _db = dbContext;

        public async Task<Game?> GetByIdAsync(ObjectId id)
        {
            return await _db.Games.FindAsync(id);
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _db.Games.ToListWithNoLockAsync();
        }

        public async Task<bool> AddAsync(Game entity)
        {
            await _db.Games.AddAsync(entity);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Game entity)
        {
            _db.Games.Update(entity);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Game entity)
        {
            _db.Games.Remove(entity);
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
