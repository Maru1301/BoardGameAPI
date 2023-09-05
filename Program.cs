using Menu_Practice.Characters;
using Menu_Practice.Menu;
using System.Security.Cryptography.X509Certificates;

namespace Menu_Practice
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            ConsoleController consoleController = new();

            consoleController.ShowLoading();

            IMenuBuilder menuBuilder = new MenuBuilder();
            MenuDirector menuDirector = new(menuBuilder);
            menuDirector.ConstructMenu();
            MenuList currentMenuList = menuBuilder.GetRootMenuList();

            MenuController menuController = new(currentMenuList);

            Status status = Status.InMenu;
            while (status != Status.End)
            {
                while(status == Status.InMenu)
                {
                    MenuOption menuOption = consoleController.GetMenuOption(currentMenuList);
                    if(currentMenuList.IsRootList && menuOption.OptionName == "Exit")
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

                if(status == Status.InGame)
                {
                    consoleController.ShowLoading();

                    (Character player, Character opponent) = menuController.GetChosenCharacterAndChosenOpponent();

                    Game game = new(player, opponent);

                    status = game.Start();
                }
            }
        }
    }
}