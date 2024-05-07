using BoardGame.Infrastractures;
using BoardGame.Models.DTOs;
using BoardGame.Models.EFModels;
using BoardGame.Repositories.Interfaces;

namespace BoardGame.Repositories
{
    public class GameRepository(AppDbContext dbContext) : IRepository, IGameRepository
    {
        private readonly AppDbContext _db = dbContext;
        public async Task AddNewGame(GameDTOs.GameInfoDTO dto)
        {
            var newGame = dto.ToEntity<Game>();
            await _db.AddAsync(newGame);
            _db.SaveChanges();
        }
    }
}
