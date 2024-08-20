using BoardGame.Infrastractures;
using MongoDB.Bson;

namespace BoardGame.Models.EFModels
{
    public class Game
    {
        public ObjectId Id { get; set; }
        public string Player1Account { get; set; } = string.Empty;
        public string Player2Account { get; set; } = string.Empty;
        public List<Character> Player1Characters { get; set; } = new();
        public List<Character> Player2Characters { get; set; } = new();
        public List<Round> Round { get; set; } = [];
        public EndGameInfo? EndGameInfo { get; set; }
        public long CreatedTime { get; set; }
    }

    public class Round
    {
        public int Order { get; set; }

        //record by member account
        public string Winner { get; set; } = string.Empty;

        //0: player first, 1: bot first
        public WhoGoesFirst WhoGoesFirst { get; set; }

        public PlayerRoundInfo Player1 { get; set; } = new();

        public PlayerRoundInfo Player2 { get; set; } = new();

        public long RoundBegin { get; set; }

        public long RoundEnd { get; set; }

        public bool InGame { get => RoundEnd == default; }
    }

    public class PlayerRoundInfo
    {
        public Character Character { get; set; }

        public CardSet Hand { get; set; } = new();

        public List<Card> ChosenCards { get; set; } = [];

        public int LastOpened { get; set; }
    }

    public class EndGameInfo
    {
        public string Winner { get; set; } = string.Empty;
        public int Player1Point { get; set; }
        public int Player2Point { get; set; }
    }
}
