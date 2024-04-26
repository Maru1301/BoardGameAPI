using BoardGame.Infrastractures;
using static BoardGame.Models.DTOs.GameDTOs;
using static BoardGame.Models.ViewModels.GameVMs;

namespace BoardGame.Services.Interfaces
{
    public interface IGameService
    {
        public void BeginNewGame(GameInfoDTO dto);
        public Func<PlayerInfoVM, PlayerInfoVM, Result> MapRule(Character character);
    }
}
