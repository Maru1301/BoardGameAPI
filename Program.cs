using System.Security.Cryptography.X509Certificates;

namespace Menu_Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            Loading.Show();

            MenuBuilder menuBuilder = new();

            menuBuilder.ConstructMenu();

            Menu menu = menuBuilder.BuildMenu();

            MenuController manipulator = new(menu);

            manipulator.ActivateMenu();
        }
    }
}