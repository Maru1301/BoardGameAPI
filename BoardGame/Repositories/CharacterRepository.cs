using BoardGame.Infrastractures;
using BoardGame.Models.EFModels;
using BoardGame.Repositories.Interfaces;
using MongoDB.Bson;

namespace BoardGame.Repositories
{
    public class CharacterRepository(AppDbContext dbContext) : ICharacterRepository
    {
        public Task<bool> AddAsync(Models.EFModels.Character entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Models.EFModels.Character entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Models.EFModels.Character>> GetAllAsync()
        {
            return await dbContext.Characters.ToListWithNoLockAsync();
        }

        public Task<Models.EFModels.Character?> GetByIdAsync(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Models.EFModels.Character entity)
        {
            throw new NotImplementedException();
        }
    }
}
