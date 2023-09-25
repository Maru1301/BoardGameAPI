using Menu_Practice.Characters;
using Menu_Practice.Characters.Builders;
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
            int removedIndex = 0;
            MenuOption removedOption = new();
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
        
        static Status RunGame(Status status, ConsoleController consoleController, MenuController menuController)
        {
            consoleController.ShowLoading();

            Character playerCharacter = menuController.GetChosenCharacter();
            Character opponentCharacter = menuController.GetChosenOpponent();

            GameController gameController = new(playerCharacter, opponentCharacter);

            gameController.BeginNewGame();

            while (status == Status.InGame)
            {
                gameController.BeginNewRound();

                List<int> playerCards = gameController.GetPlayerCards();
                List<int> npcCards = gameController.GetNPCCards();

                int playerChosenCard = consoleController.GetPlayerChosenCard(playerCards);
                int npcChosenCard = gameController.GetNPCChosenCard();
                consoleController.ShowChosenCards(playerChosenCard, npcChosenCard);
                PlayerInfoContainer playerInfoContainer = new(playerCards, playerChosenCard);
                PlayerInfoContainer npcInfoContainer = new(npcCards, npcChosenCard);
                Result result = gameController.JudgeRound(playerInfoContainer, npcInfoContainer);

                Card card = Card.None;
                if(result == Result.BasicWin)
                {
                    card = consoleController.GetPlayerWinCard(npcInfoContainer.Cards);
                }
                else if(result == Result.BasicLose)
                {
                    card = gameController.GetNPCWinCard();
                }
                else if(result == Result.CharacterRuleWin)
                {
                    card = (Card)npcChosenCard;
                }
                else if(result == Result.CharacterRuleLose)
                {
                    card = (Card)playerChosenCard;
                }

                gameController.ProcessSettlement(result, card);
                consoleController.ShowRoundResult(result, card);

                gameController.EndRound();
            }

            //var outcome = gameController.GetOutcome();

            //consoleController.ShowOutcome(outcome);

            return status;
        }
    }
}