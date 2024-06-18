namespace Menu_Practice;

internal partial class Program
{
    private static async Task Main(string[] args)
    {
        MenuService menuService = new();

        var system = new SystemController(menuService);

        await system.Start();
    }
}