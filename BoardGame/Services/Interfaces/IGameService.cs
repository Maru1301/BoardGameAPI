using BoardGame.Infrastractures;
using BoardGame.Models.EFModels;
using BoardGame.Models.DTOs;

namespace BoardGame.Services.Interfaces
{
    public interface IGameService
    {
        public Task<IEnumerable<GameDTO>> GetGameRecordList();

        public Task<string> PlayWithBot(string account);

        public Task<(bool gameStarted, string roomId)> Match(string account);

        //public Task<string> HostGame();

        //public Task<string> JoinGame();

        //public Task<bool> GetReady();

        public Func<PlayerRoundInfo, PlayerRoundInfo, Result> MapRule(Character character);

        Task EndGame(string currentGameId, string userAccount);
        Task BeginNewRound(RoundInfoDTO roundInfo, string userAccount);
        Task<Card> OpenNextCard(OpenNextCardRequestDTO dto);
        Task EndRound(Character playerChosenCharacter, string userAccount);
    }
}
