namespace Menu_Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MenuList mainmenu = new MenuList();
            MenuOption mainmenuOption = new("Start");
            MenuOption mainmenuOption2 = new("Exit");
            mainmenu.Push(mainmenuOption);
            mainmenu.Push(mainmenuOption2);
            mainmenu.Show();
        }
    }
}