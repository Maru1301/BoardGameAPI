
using BoardGame.Controllers;
using BoardGame.Infrastractures;

namespace BoardGame.Models.ViewModels
{
    public class GameVMs
    {
        public class GameInfoVM
        {
            public string Account { get; set; } = string.Empty;

            public CharacterSet Player { get; set; } = new();

            public CharacterSet Bot { get; set; } = new();

            public DateTime CreatedTime { get; set; }

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
