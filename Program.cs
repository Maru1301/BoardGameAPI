using Menu_Practice.Characters;
using Menu_Practice.Menu;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using static Menu_Practice.Game;
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
                status = OperateMenu(status, consoleController, currentMenuList, menuController);

                if(status == Status.InGame)
                {
                    consoleController.ShowLoading();

                    (Character player, Character opponent) = menuController.GetChosenCharacterAndChosenOpponent();

                    Game game = new(player, opponent);

                    status = game.Start();

                    bool playerGoFirst = true;
                    while (status == Status.InGame)
                    {
                        Round round;
                        //todo start each round one by one
                        if (game.WhoGoFirst() == playerGoFirst)
                        {
                            round = new(_player.Character.UseRuleLogic);
                            _player.GoFirst = false;
                            _npc.GoFirst = true;
                        }
                        else
                        {
                            round = new(_npc.Character.UseRuleLogic);
                            _npc.GoFirst = false;
                            _player.GoFirst = true;
                        }

                        var playerCards = game.GetPlayerCards();

                        var playerChosenCard = consoleController.GetPlayerChosenCard(playerCards);
                        var npcChosenCard = round.GetNPCChosenCard();

                        round.Judge(playerChosenCard, npcChosenCard);
                    }
                }
            }
        }

        private static MenuList GenerateMenu()
        {
            IMenuBuilder menuBuilder = new MenuBuilder();
            MenuDirector menuDirector = new(menuBuilder);
            menuDirector.ConstructMenu();

            return menuBuilder.GetRootMenuList();
        }

        private static Status OperateMenu(Status status, ConsoleController consoleController, MenuList currentMenuList, MenuController menuController)
        {
            while (status == Status.InMenu)
            {
                MenuOption menuOption = consoleController.GetMenuOption(currentMenuList);
                if (currentMenuList.IsRootList && menuOption.OptionName == "Exit")
                {
                    status = Status.End;
                    break;
                }

                if (currentMenuList.IsLastMenuList && menuOption.OptionName == "Select")
                {
                    status = Status.InGame;
                }

                currentMenuList = menuController.GetNextMenuList(menuOption);
            }

            return status;
        }
    }
}