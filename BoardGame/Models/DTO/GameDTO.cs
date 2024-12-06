using BoardGame.Infrastractures;
using BoardGame.Models.EFModels;

namespace BoardGame.Models.DTO;

// Base class for shared properties
public class GameBaseDTO
{
    public string Player1Account { get; set; } = string.Empty;
    public string Player2Account { get; set; } = string.Empty;
    public List<Infrastractures.Character> Player1Characters { get; set; } = [];
    public List<Infrastractures.Character> Player2Characters { get; set; } = [];
}

public class GameDTO : GameBaseDTO
{
    public int CurrentRound { get; set; } = 0;
    public List<Round> Round { get; set; } = [];
    public EndGameInfo EndGameInfo { get; set; } = new();
    public long CreatedTime { get; set; }
}

public class GameInfoRequestDTO : GameBaseDTO
{
}

public class GameInfoDTO : GameBaseDTO
{
    public string CurrentGameId { get; set; } = string.Empty;
    public WhoGoesFirst WhoGoesFirst { get; set; }
    public long CreatedTime { get; set; } = Extensions.GetTimeStamp();
}

public class RoundInfoRequestDTO
{
    public string CurrentGameId { get; set; } = string.Empty;
    public PlayerRoundInfo Player1 { get; set; } = new();
    public PlayerRoundInfo Player2 { get; set; } = new();
}

public class RoundInfoResponseDTO
{
    public string Winner { get; set; } = string.Empty;
    public PlayerRoundInfo Player1 { get; set; } = new();
    public PlayerRoundInfo Player2 { get; set; } = new();
}

public class RoundInfoDTO
{
    public string CurrentGameId { get; set; } = string.Empty;
    public string Winner { get; set; } = string.Empty;
    public WhoGoesFirst WhoGoesFirst { get; set; }
    public PlayerRoundInfo Player1 { get; set; } = new();
    public PlayerRoundInfo Player2 { get; set; } = new();
    public long RoundStart { get; set; }
    public long RoundEnd { get; set; }
    public Infrastractures.Character RuleCharacter => WhoGoesFirst == WhoGoesFirst.Player1 ? Player1.Character : Player2.Character;
}

public class OpenNextCardRequestDTO
{
    public string CurrentGameId { get; set; } = string.Empty;
    public int RoundOrder { get; set; }
    public string UserAccount { get; set; } = string.Empty;
}
