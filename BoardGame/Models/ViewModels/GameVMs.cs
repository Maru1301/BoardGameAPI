
using BoardGame.Infrastractures;

namespace BoardGame.Models.ViewModels
{

    public class GameVMs
    {
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
