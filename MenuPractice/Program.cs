namespace Menu_Practice;

internal partial class Program
{
    private static async Task Main(string[] args)
    {
        MenuService menuService = new();
        GameService gameService = new(new Characters.Character(), new Characters.Character());

        var system = new SystemController(menuService, gameService);

        await system.Start();
    }
}