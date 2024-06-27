using BoardGame.Infrastractures;
using BoardGame.Models.EFModels;
using BoardGame.Models.DTOs;

namespace BoardGame.Services.Interfaces
{
    public interface IGameService
    {
        public Task<IEnumerable<GameDTO>> GetGameRecordList();

        public Task<string> PlayWithBot(WhoGoesFirst whoGoesFirst, string account);

        public Task<(bool gameStarted, string currentGameId)> Match(string account);

        public Task PickCharacter(string currentGameId, string account);

        //public Task<string> HostGame();

        //public Task<string> JoinGame();

        //public Task<bool> GetReady();

        public Func<PlayerRoundInfo, PlayerRoundInfo, Result> MapRule(Character character);

        Task EndGame(string currentGameId, string userAccount);
        Task BeginNewRound(RoundInfoDTO roundInfo, string userAccount);
        Task<(Card, bool)> OpenNextCard(OpenNextCardRequestDTO dto);
        Task<(RoundInfoDTO, bool)> EndRound(string currentGameId, string userAccount);
    }
}
