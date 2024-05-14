using BoardGame.Infrastractures;
using BoardGame.Models.EFModels;
using MongoDB.Bson;

namespace BoardGame.Models.ViewModels
{
    public class GameVMs
    {
        public class GameVM
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

        public class GameInfoVM
        {
            public string Player1Account { get; set; } = string.Empty;
            public string Player2Account { get; set; } = string.Empty;
            public CharacterSet Player1Characters { get; set; } = new();
            public CharacterSet Player2Characters { get; set; } = new();
        }

        public class RoundInfoVM
        {
            public ObjectId Id { get; set; }
            public PlayerRoundInfo Player1 { get; set; } = new();
            public PlayerRoundInfo Player2 { get; set; } = new();
        }

        public class PlayerInfoVM(CardSet cardSet, Card currentChosen)
        {
            public CardSet CardSet { get; set; } = cardSet;
            public Card CurrentChosenCard { get; set; } = currentChosen;
        }
    }
}
