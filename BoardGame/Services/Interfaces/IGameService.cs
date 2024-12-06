using BoardGame.Infrastractures;
using BoardGame.Models.EFModels;
using BoardGame.Models.DTO;

namespace BoardGame.Services.Interfaces
{
    public interface IGameService
    {
        Task<IEnumerable<GameDTO>> GetGameRecordList();

        Task<IEnumerable<CharacterDTO>> GetCharacterList();

        Task<string> PlayWithBot(WhoGoesFirst whoGoesFirst, string account);

        Task<(bool gameStarted, string currentGameId)> Match(string account);

        Task PickCharacter(string currentGameId, string account);

        //public Task<string> HostGame();

        //public Task<string> JoinGame();

        //public Task<bool> GetReady();

        Func<PlayerRoundInfo, PlayerRoundInfo, GameResult> MapRule(Infrastractures.Character character);

        Task EndGame(string currentGameId, string userAccount);
        Task BeginNewRound(RoundInfoDTO roundInfo, string userAccount);
        Task<(Card, bool)> OpenNextCard(OpenNextCardRequestDTO dto);
        Task<(RoundInfoDTO, bool)> EndRound(string currentGameId, string userAccount);
    }
}
