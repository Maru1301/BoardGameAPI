using MenuVisualizer;

namespace BoardGameClient;

public class SystemController
{
    private readonly GameService _gameService;
    private string _token = string.Empty;
    private readonly string _domain = Resources.TempDomainName;
    private Character player = new();
    private Character bot = new();

    private object? SetPlayerCharacter(object? obj)
    {
        player = (Character)obj;
        return null;
    }

    private object? SetBotCharacter(object? obj)
    {
        bot = (Character)obj;
        return null;
    }

    public SystemController()
    {
        var builder = new NewMenuBuilder(SetPlayerCharacter, SetBotCharacter);
        var menu = builder.Construct();
        var manager = new ConsoleMenuManager();
        manager.Construct(menu);

        manager.ShowAsync();
    }

    public async Task Start()
    {

#if DEBUG

#else
        while (await LoginAsync() == false) { };
        await _gameService.Test(_token);
#endif
    }

    // Refactoring required
    //private async Task<bool> LoginAsync()
    //{
    //    //show login hint on console
    //    var (account, password) = ConsoleService.GetUserCredentials();

    //    account = string.IsNullOrEmpty(account) ? "93220allen" : account;
    //    password = string.IsNullOrEmpty(password) ? "allen93220" : password;

    //    var client = new HttpClient();
    //    client.SendAsync
    //    var request = new RestRequest("api/Member/Login");
    //    object payload = new
    //    {
    //        account,
    //        password
    //    };
    //    request.AddBody(payload);

    //    var loadingTask = ConsoleService.ShowLoading();
    //    try
    //    {
    //        var result = client.Post(request);

    //        if (result.Content == null)
    //        {
    //            Console.WriteLine("token missing");
    //            return false;
    //        }

    //        var r = JsonSerializer.Deserialize<Res>(result.Content);

    //        if (r == null)
    //        {
    //            Console.WriteLine("Parse Failed");
    //            return false;
    //        }

    //        _token = r.token;

    //        ConsoleService.StopShowLoading();
    //        await loadingTask;
    //        return true;
    //    }
    //    catch (Exception)
    //    {
    //        ConsoleService.StopShowLoading();
    //        await loadingTask;

    //        Console.Clear();
    //        Console.WriteLine("Login Failed");
    //        return false;
    //    }
    //}
}

public class Res
{
    public string token { get; set; } = string.Empty;
}
