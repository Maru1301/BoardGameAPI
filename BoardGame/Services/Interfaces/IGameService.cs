using static BoardGame.Models.DTOs.GameDTOs;

namespace BoardGame.Services.Interfaces
{
    public interface IGameService
    {
        public Task BeginNewGame(GameInfoDTO dto, string userAccount);
    }
}
