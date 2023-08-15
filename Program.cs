namespace Menu_Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MenuBuilder menuBuilder = new();

            menuBuilder.ConstructMenu();

            Menu menu = menuBuilder.BuildMenu();

            menu.Show();
        }
    }
}