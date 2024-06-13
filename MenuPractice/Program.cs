namespace Menu_Practice;

internal partial class Program
{
    private static async Task Main(string[] args)
    {
        ConsoleService consoleService = new();
        MenuService menuService = new();

        var system = new SystemController(consoleService, menuService);

        while (await system.LoginAsync() == false);

        var status = Status.InMenu;
        while (status != Status.End)
        {
            status = system.RunMenu(status);

            if (status == Status.InGame)
            {
                status = system.RunGame(status);
            }
        }
    }
}