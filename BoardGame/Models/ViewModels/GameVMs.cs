using BoardGame.Infrastractures;
using BoardGame.Models.EFModels;
using MongoDB.Bson;

namespace BoardGame.Models.ViewModels
{
    public class GameVMs
    {
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
