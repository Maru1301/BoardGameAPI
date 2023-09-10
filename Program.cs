using Menu_Practice.Characters;
using Menu_Practice.Menu;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using static Menu_Practice.GameController;
using static Menu_Practice.Program;

namespace Menu_Practice
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            
            MenuList currentMenuList = GenerateMenu();

            MenuController menuController = new(currentMenuList);

            ConsoleController consoleController = new();
            consoleController.ShowLoading();

            Status status = Status.InMenu;
            while (status != Status.End)
            {
                status = RunMenu(status, consoleController, currentMenuList, menuController);

                if (status == Status.InGame)
                {
                    RunGame(status, consoleController, menuController);
                }
            }
        }

        static MenuList GenerateMenu()
        {
            IMenuBuilder menuBuilder = new MenuBuilder();
            MenuDirector menuDirector = new(menuBuilder);
            menuDirector.ConstructMenu();

            return menuBuilder.GetRootMenuList();
        }

        static Status RunMenu(Status status, ConsoleController consoleController, MenuList currentMenuList, MenuController menuController)
        {
            while (status == Status.InMenu)
            {
                MenuOption menuOption = consoleController.GetMenuOption(currentMenuList);
                if (currentMenuList.IsRootList && menuOption.OptionName == "Exit")
                {
                    status = Status.End;
                    break;
                }

                if(currentMenuList.GetType() ==  typeof(CharacterInfoMenu) && menuOption.OptionName == "Select")
                {
                    menuController.SetChosenCharacter(((CharacterInfoMenu)currentMenuList).Character);
                }
                else if(currentMenuList.GetType() == typeof(OpponentMenu) && menuOption.OptionName != "Back")
                {
                    menuController.SetChosenOpponent(((OpponentMenuOption)menuOption).Character);
                    status = Status.InGame;
                    break;
                }

                if (menuOption.OptionName == "Back")
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
        
        static Status RunGame(Status status, ConsoleController consoleController, MenuController menuController)
        {
            consoleController.ShowLoading();

            (Character playerCharacter, Character opponentCharacter) = menuController.GetChosenCharacterAndChosenOpponent();

            GameController gameController = new(playerCharacter, opponentCharacter);

            gameController.BeginNewGame();

            while (status == Status.InGame)
            {
                Round round = gameController.BeginNewRound();

                List<int> playerCards = gameController.GetPlayerCards();
                List<int> npcCards = gameController.GetNPCCards();

                int playerChosenCard = consoleController.GetPlayerChosenCard(playerCards);
                int npcChosenCard = gameController.GetNPCChosenCard();
                PlayerInfoContainer playerInfoContainer = new(playerCards, playerChosenCard);
                PlayerInfoContainer npcInfoContainer = new(npcCards, npcChosenCard);
                round.Judge(playerInfoContainer, npcInfoContainer);
            }

            return status;
        }
    }
}