using Menu_Practice.Characters;
using Menu_Practice.Menu;

namespace Menu_Practice
{
    internal partial class Program
    {
        private static void Main(string[] args)
        {
            Console.CursorVisible = false;
            
            ConsoleController.ShowLoading();

            var currentMenuList = GenerateMenu();

            MenuController menuController = new(currentMenuList);

            ConsoleController consoleController = new();

            var status = Status.InMenu;
            while (status != Status.End)
            {
                status = RunMenu(status, consoleController, currentMenuList, menuController);

                if (status == Status.InGame)
                {
                    status = RunGame(status, consoleController, menuController);
                }
            }
        }

        private static MenuList GenerateMenu()
        {
            CharacterList characterList = new();
            IMenuBuilder menuBuilder = new MenuBuilder(characterList);
            MenuDirector menuDirector = new(menuBuilder);
            menuDirector.ConstructMenu();

            return menuBuilder.GetRootMenuList();
        }

        private static Status RunMenu(Status status, ConsoleController consoleController, MenuList currentMenuList, MenuController menuController)
        {
            var removedIndex = 0;
            MenuOption removedOption = new();
            while (status == Status.InMenu)
            {
                var menuOption = consoleController.GetMenuOption(currentMenuList);
                if (currentMenuList.IsRootList && menuOption.OptionName == "Exit")
                {
                    status = Status.End;
                    break;
                }

                if(currentMenuList.GetType() ==  typeof(CharacterInfoMenu) && menuOption.OptionName == "Select")
                {
                    menuController.SetChosenCharacter(((CharacterInfoMenu)currentMenuList).Character);
                    currentMenuList = menuController.GetNextMenuList(menuOption);
                    (removedOption, removedIndex) = ((OpponentMenu)currentMenuList).FilterChosenCharacter(menuController.GetChosenCharacter());
                }
                else if(currentMenuList.GetType() == typeof(OpponentMenu))
                {
                    if(menuOption.OptionName == "Back")
                    {
                        currentMenuList.Insert(removedIndex, removedOption);
                        currentMenuList = menuController.GetPrevMenuList();
                    }
                    else
                    {
                        menuController.SetChosenOpponent(((OpponentMenuOption)menuOption).Character);
                        status = Status.InGame;
                        break;
                    }
                }
                else if (menuOption.OptionName == "Back")
                {
                    currentMenuList = menuController.GetPrevMenuList();
                }
                else
                {
                    currentMenuList = menuController.GetNextMenuList(menuOption);
                }
            }

            return status;
        }

        private static Status RunGame(Status status, ConsoleController consoleController, MenuController menuController)
        {
            ConsoleController.ShowLoading();

            var playerCharacter = menuController.GetChosenCharacter();
            var opponentCharacter = menuController.GetChosenOpponent();

            GameController gameController = new(playerCharacter, opponentCharacter);

            gameController.BeginNewGame();

            while (status == Status.InGame)
            {
                gameController.BeginNewRound();

                var playerCards = gameController.GetPlayerCards();
                var npcCards = gameController.GetNpcCards();

                var playerChosenCard = consoleController.GetPlayerChosenCard(playerCards);
                var npcChosenCard = gameController.GetNpcChosenCard();
                ConsoleController.ShowChosenCards(playerChosenCard, npcChosenCard);
                PlayerInfoContainer playerInfoContainer = new(playerCards, playerChosenCard);
                PlayerInfoContainer npcInfoContainer = new(npcCards, npcChosenCard);
                Result result = gameController.JudgeRound(playerInfoContainer, npcInfoContainer);

                var card = result switch
                {
                    Result.BasicWin => consoleController.GetPlayerWinCard(npcInfoContainer.Cards),
                    Result.BasicLose => gameController.GetNpcWinCard(),
                    Result.CharacterRuleWin => (Card)npcChosenCard,
                    Result.CharacterRuleLose => (Card)playerChosenCard,
                    _ => Card.None
                };

                gameController.ProcessSettlement(result, card);
                ConsoleController.ShowRoundResult(result, card);

                status = gameController.EndRound();
            }

            var outcome = GameController.GetOutcome();

            ConsoleController.Show(outcome);

            return status;
        }
    }
}