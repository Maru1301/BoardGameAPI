namespace BoardGameClient;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        ArgumentNullException.ThrowIfNull(args);

        //MenuService menuService = new();
        //GameService gameService = new(new Characters.Character(), new Characters.Character());

        var system = new SystemController();

        await system.Start();
    }
}