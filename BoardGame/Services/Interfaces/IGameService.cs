using BoardGame.Infrastractures;
using BoardGame.Models.EFModels;
using static BoardGame.Models.DTOs.GameDTOs;

namespace BoardGame.Services.Interfaces
{
    public interface IGameService
    {
        public Task BeginNewGame(GameInfoDTO dto, string userAccount);

        public Func<PlayerRoundInfo, PlayerRoundInfo, Result> MapRule(Character character);
    }
}
