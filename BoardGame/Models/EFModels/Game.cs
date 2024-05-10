using BoardGame.Infrastractures;
using MongoDB.Bson;

namespace BoardGame.Models.EFModels
{
    public class Game
    {
        public ObjectId Id { get; set; }
        public string Player1Account { get; set; } = string.Empty;
        public string Player2Account { get; set; } = string.Empty;
        public CharacterSet Player1Characters { get; set; } = new();
        public CharacterSet Player2Characters { get; set; } = new();
        public Round? Round1 { get; set; }
        public Round? Round2 { get; set; }
        public Round? Round3 { get; set; }
        public Round? Round4 { get; set; }
        public Round? Round5 { get; set; }
        public Round? Round6 { get; set; }
        public EndGameInfo EndGameInfo { get; set; } = new();
        public long CreatedTime { get; set; }
    }

    public class CharacterSet
    {
        public Character Character1 { get; set; }

        public Character Character2 { get; set; }

        public Character Characetr3 { get; set; }
    }

    public class Round
    {
        //record by member account
        public string Winner { get; set; } = string.Empty;

        //0: player first, 1: bot first
        public WhoGoesFirst WhoGoesFirst { get; set; }

        public PlayerRoundInfo Player1 { get; set; } = new();

        public PlayerRoundInfo Player2 { get; set; } = new();

        public long RoundBegin { get; set; }

        public long RoundEnd { get; set; }
    }

    public class PlayerRoundInfo
    {
        //public int CompressedInfo { get; set; } = 0;
        public Character Character { get; set; }

        public CardSet Hand { get; set; } = new();

        public RoundCards ChosenCards { get; set; } = new();
    }

    public class RoundCards
    {
        public Card Card1 { get; set; }
        public Card Card2 { get; set; }
        public Card Card3 { get; set; }
        public int LastOpened { get; set; }
    }

    public class EndGameInfo
    {
        public string Winner { get; set; } = string.Empty;
        public int Player1Point { get; set; }
        public int Player2Point { get; set; }
    }
}
