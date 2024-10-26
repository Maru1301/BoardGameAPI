using BoardGameClient.Characters;
using BoardGameClient.Menu;

namespace BoardGameClient;

public class SystemController
{
    private readonly MenuService _menuService;
    private readonly GameService _gameService;
    private string _token = string.Empty;
    private readonly string _domain = Resources.TempDomainName;

    public SystemController(MenuService menuService, GameService gameService)
    {
        Console.CursorVisible = false;
        _menuService = menuService; // Todo: menuServire requires Refactoring
        _gameService = gameService;

        menuService.Init(GenerateMenu());
    }

    public async Task Start()
    {

#if DEBUG

#else
        while (await LoginAsync() == false) { };
        await _gameService.Test(_token);
#endif

        var status = Status.InMenu;
        while (status != Status.End)
        {
            status = RunMenu(status);
            if (status == Status.InGame)
            {
                status = RunGame(status);
            }
        }
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

    private static MenuList GenerateMenu()
    {
        CharacterList characterList = new();
        IMenuBuilder menuBuilder = new MenuBuilder(characterList);
        MenuDirector menuDirector = new(menuBuilder);
        menuDirector.ConstructMenu();

        return menuBuilder.GetRootMenuList();
    }

    private Status RunMenu(Status status)
    {
        var removedIndex = 0;
        MenuOption removedOption = new();
        while (status == Status.InMenu)
        {
            var menuOption = _menuService.CurrentMenuList.GetMenuOption();
            if (_menuService.IsCurrentMenuListRoot() && menuOption.OptionName == "Exit")
            {
                status = Status.End;
                break;
            }

            if (_menuService.GetCurrentMenuList().GetType() == typeof(CharacterInfoMenu) && menuOption.OptionName == "Select")
            {
                _menuService.SetChosenCharacter(((CharacterInfoMenu)_menuService.GetCurrentMenuList()).Character);
                _menuService.MoveToNextMenuList(menuOption);
                (removedOption, removedIndex) = ((OpponentMenu)_menuService.GetCurrentMenuList()).FilterChosenCharacter(_menuService.GetChosenCharacter());
            }
            else if (_menuService.GetCurrentMenuList().GetType() == typeof(OpponentMenu))
            {
                if (menuOption.OptionName == "Back")
                {
                    _menuService.GetCurrentMenuList().Insert(removedIndex, removedOption);
                    _menuService.MoveToPrevMenuList();
                }
                else
                {
                    _menuService.SetChosenOpponent(((OpponentMenuOption)menuOption).Character);
                    status = Status.InGame;
                    break;
                }
            }
            else if (menuOption.OptionName == "Back")
            {
                _menuService.MoveToPrevMenuList();
            }
            else
            {
                _menuService.MoveToNextMenuList(menuOption);
            }
        }

        return status;
    }

    private Status RunGame(Status status)
    {
        var playerCharacter = _menuService.GetChosenCharacter();
        var opponentCharacter = _menuService.GetChosenOpponent();

        var gameService = new GameService(playerCharacter, opponentCharacter);

        gameService.BeginNewGame();

        while (status == Status.InGame)
        {
            gameService.BeginNewRound();

            var (playerChosenCard, npcChosenCard) = gameService.GetChosenCard();
            (playerChosenCard, npcChosenCard).ShowChosenCards();
            Result result = gameService.JudgeRound();

            var card = result switch
            {
                Result.BasicWin => gameService.GetPlayerWinCard(),
                Result.BasicLose => gameService.GetNpcWinCard(),
                Result.CharacterRuleWin => npcChosenCard,
                Result.CharacterRuleLose => playerChosenCard,
                Result.Draw => Card.None,
                _ => throw new NotImplementedException()
            };

            gameService.ProcessSettlement(result, card);
            (result, card).ShowRoundResult();

            status = gameService.EndRound();
        }

        gameService.GetOutcome().Show();

        return status;
    }
}

public class Res
{
    public string token { get; set; } = string.Empty;
}
