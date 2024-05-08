using BoardGame.Models.EFModels;
using BoardGame.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BoardGame.Repositories
{
    public class GameRepository(AppDbContext dbContext) : IGameRepository
    {
        public async Task<Game?> GetByIdAsync(string id)
        {
            return await _db.Games.FindAsync(id);
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _db.Games.ToListAsync();
        }

        private readonly AppDbContext _db = dbContext;
        public async Task AddAsync(Game dto)
        {
            await _db.Games.AddAsync(dto);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Game entity)
        {
            _db.Games.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
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
