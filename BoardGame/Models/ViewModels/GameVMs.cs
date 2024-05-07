
using BoardGame.Controllers;
using BoardGame.Infrastractures;

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

        }
        public class NewGameInfoVM
        {
            public WhoGoesFirst WhoGoesFirst { get; set; }
            public string Message { get; set; } = string.Empty;
        }

        public class PlayerInfoVM(CardSet cardSet, Card currentChosen)
        {
            public CardSet CardSet { get; set; } = cardSet;

            public Card CurrentChosenCard { get; set; } = currentChosen;
        }
    }
}
