namespace Menu_Practice;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        if (args is null)
        {
            throw new ArgumentNullException(nameof(args));
        }

        MenuService menuService = new();
        GameService gameService = new(new Characters.Character(), new Characters.Character());

        var system = new SystemController(menuService, gameService);

        await system.Start();
    }
}