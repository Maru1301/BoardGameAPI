using BoardGame.Controllers;
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
        public Round Round1 { get; set; } = new();
        public Round Round2 { get; set; } = new();
        public Round Round3 { get; set; } = new();
        public Round Round4 { get; set; } = new();
        public Round Round5 { get; set; } = new();
        public Round Round6 { get; set; } = new();
        public long CreatedTime { get; set; }
    }

    public class Round
    {
        //record by member account
        public string Winner { get; set; } = string.Empty;

        //0: player first, 1: bot first
        public WhoGoesFirst WhoGoesFirst { get; set; }

        public PlayerInfo Player1 { get; set; } = new();

        public PlayerInfo Player2 { get; set; } = new();

        public long RoundStart { get; set; }

        public long RoundEnd { get; set; }
    }

    public class PlayerInfo
    {
        public Character Character { get; set; }

        public CardSet Hand { get; set; } = new();

        public RoundCards ChosenCards { get; set; }
    }

    public enum RoundCards
    {
        card1,
        card2,
        card3
    }
}
