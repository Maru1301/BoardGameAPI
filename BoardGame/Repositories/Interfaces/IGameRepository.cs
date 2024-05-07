using static BoardGame.Models.DTOs.GameDTOs;

namespace BoardGame.Repositories.Interfaces
{
    public interface IGameRepository
    {
        public Task AddNewGame(GameInfoDTO dto);
    }
}
