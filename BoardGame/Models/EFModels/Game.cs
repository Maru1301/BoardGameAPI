using BoardGame.Controllers;
using BoardGame.Infrastractures;
using MongoDB.Bson;
using MongoDB.Driver.Core.Operations;

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
        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }

    public class Round
    {
        //record by member account
        public string Winner { get; set; } = string.Empty;

        //0: player first, 1: bot first
        public WhoGoesFirst WhoGoesFirst { get; set; }

        public Character Player1Character { get; set; }

        public CardSet Player1Hand { get; set; } = new();

        public RoundCards Player1ChosenCard { get; set; }

        public Character Player2Character { get; set; }

        public CardSet Player2Hand { get; set; } = new();

        public RoundCards Player2ChosenCard { get; set; }

        public Character RuleCharacter { get => WhoGoesFirst == WhoGoesFirst.Player1 ? Player1Character : Player2Character; }

        public DateTime RoundStart { get; set; } = DateTime.Now;

        public DateTime RoundEnd { get; set; }
    }

    public enum RoundCards
    {
        card1,
        card2,
        card3
    }
}
