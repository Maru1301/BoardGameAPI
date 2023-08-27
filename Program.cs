using Menu_Practice.Characters;
using System.Security.Cryptography.X509Certificates;

namespace Menu_Practice
{
    internal partial class Program
    {

        private Status _status;
        static void Main(string[] args)
        {

            Console.CursorVisible = false;

            Loading.Show();

            MenuBuilder menuBuilder = new();

            menuBuilder.ConstructMenu();

            var rootMenuList = menuBuilder.GetRootMenuList();

            MenuController controller = new(rootMenuList);

            controller.ActivateMenu();

            (Character character, Character opponent) = controller.GetChosenCharacterAndChosenOpponent();

            Game game = new(character, opponent);

            game.Start();
        }
    }
}