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
                    consoleController.ShowLoading();

                    (Character playerCharacter, Character opponentCharacter) = menuController.GetChosenCharacterAndChosenOpponent();

                    GameController gameController = new(playerCharacter, opponentCharacter);
                    
                    gameController.BeginNewGame();

                    while (status == Status.InGame)
                    {
                        Round round = gameController.BeginNewRound();

                        List<int> playerCards = gameController.GetPlayerCards();

                        int playerChosenCard = consoleController.GetPlayerChosenCard(playerCards);
                        int npcChosenCard = gameController.GetNPCChosenCard();
                        round.Judge(playerChosenCard, npcChosenCard);
                    }

                    //    bool playerGoFirst = true;
                    //    while (status == Status.InGame)
                    //    {
                    //        Round round;
                    //        //todo start each round one by one
                    //        if (game.WhoGoFirst() == playerGoFirst)
                    //        {
                    //            round = new(_player.Character.UseRuleLogic);
                    //            _player.GoFirst = false;
                    //            _npc.GoFirst = true;
                    //        }
                    //        else
                    //        {
                    //            round = new(_npc.Character.UseRuleLogic);
                    //            _npc.GoFirst = false;
                    //            _player.GoFirst = true;
                    //        }

                    //        var playerCards = game.GetPlayerCards();

                    //       var playerChosenCard = consoleController.GetPlayerChosenCard(playerCards);
                    //        var npcChosenCard = round.GetNPCChosenCard();

                    //        round.Judge(playerChosenCard, npcChosenCard);
                    //    }
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

                if (menuOption.OptionName == "Select" && menuOption.NextMenuList == null)
                {
                    status = Status.InGame;
                    break;
                }

                if(currentMenuList.GetType() ==  typeof(CharacterInfoMenu) && menuOption.OptionName == "Select")
                {
                    menuController.SetChosenCharacter(((CharacterInfoMenu)currentMenuList).Character);
                }
                else if(currentMenuList.GetType() == typeof(OpponentMenu) && menuOption.OptionName != "Back")
                {
                    menuController.SetChosenOpponent(((OpponentMenuOption)menuOption).Character);
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
    }
}