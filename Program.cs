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

            Loading.Show();

            IMenuBuilder menuBuilder = new MenuBuilder();

            MenuDirector menuDirector = new(menuBuilder);

            menuDirector.ConstructMenu();

            var rootMenuList = menuBuilder.GetRootMenuList();

            MenuController menuController = new(rootMenuList);
            ConsoleController consoleController = new();
            consoleController.SetCurrentMenuList(rootMenuList);
            consoleController.ShowMenuList();

            Status status = Status.InMenu;
            while(status != Status.End)
            {
                status = menuController.ActivateMenu();

                if (status == Status.InGame)
                {
                    Loading.Show();

                    (Character character, Character opponent) = menuController.GetChosenCharacterAndChosenOpponent();

                    Game game = new(character, opponent);

                    status = game.Start();
                }
            }
        }
    }
}