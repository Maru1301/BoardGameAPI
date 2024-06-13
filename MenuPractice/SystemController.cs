using Menu_Practice.Characters;
using Menu_Practice.Menu;
using RestSharp;
using System.Text.Json;

namespace Menu_Practice
{
    public class SystemController
    {
        private readonly ConsoleService _consoleService;
        private readonly MenuService _menuService;
        private string _token = string.Empty;
        private MenuList _currentMenuList;

        public SystemController(ConsoleService consoleService, MenuService menuService)
        {
            Console.CursorVisible = false;
            _consoleService = consoleService;
            _menuService = menuService;

            _currentMenuList = GenerateMenu();
            menuService.Init(_currentMenuList);
        }

        public async Task<bool> LoginAsync()
        {
            //show login hint on console
            var (account, password) = _consoleService.GetUserCredentials();

            account = string.IsNullOrEmpty(account) ? "93220allen" : account;
            password = string.IsNullOrEmpty(password) ? "allen93220" : password;

            var url = "https://localhost:44318/";
            var client = new RestClient(url);
            var request = new RestRequest("api/Member/Login");
            object payload = new
            {
                account,
                password
            };
            request.AddBody(payload);

            var loadingTask = ConsoleService.ShowLoading();
            try
            {
                var result = client.Post(request);

                if (result.Content == null)
                {
                    Console.WriteLine("token missing");
                    return false;
                }

                var r = JsonSerializer.Deserialize<Res>(result.Content);

                if (r == null)
                {
                    Console.WriteLine("Parse Failed");
                    return false;
                }

                _token = r.Token;

                ConsoleService.StopShowLoading();
                await loadingTask;
            }
            catch (Exception)
            {
                ConsoleService.StopShowLoading();
                await loadingTask;

                Console.Clear();
                Console.WriteLine("Login Failed");
                return false;
            }

            return true;
        }

        private static MenuList GenerateMenu()
        {
            CharacterList characterList = new();
            IMenuBuilder menuBuilder = new MenuBuilder(characterList);
            MenuDirector menuDirector = new(menuBuilder);
            menuDirector.ConstructMenu();

            return menuBuilder.GetRootMenuList();
        }

        public Status RunMenu(Status status)
        {
            var removedIndex = 0;
            MenuOption removedOption = new();
            while (status == Status.InMenu)
            {
                var menuOption = _consoleService.GetMenuOption(_currentMenuList);
                if (_currentMenuList.IsRootList && menuOption.OptionName == "Exit")
                {
                    status = Status.End;
                    break;
                }

                if (_currentMenuList.GetType() == typeof(CharacterInfoMenu) && menuOption.OptionName == "Select")
                {
                    _menuService.SetChosenCharacter(((CharacterInfoMenu)_currentMenuList).Character);
                    _currentMenuList = _menuService.GetNextMenuList(menuOption);
                    (removedOption, removedIndex) = ((OpponentMenu)_currentMenuList).FilterChosenCharacter(_menuService.GetChosenCharacter());
                }
                else if (_currentMenuList.GetType() == typeof(OpponentMenu))
                {
                    if (menuOption.OptionName == "Back")
                    {
                        _currentMenuList.Insert(removedIndex, removedOption);
                        _currentMenuList = _menuService.GetPrevMenuList();
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
                    _currentMenuList = _menuService.GetPrevMenuList();
                }
                else
                {
                    _currentMenuList = _menuService.GetNextMenuList(menuOption);
                }
            }

            return status;
        }

        public Status RunGame(Status status)
        {
            ConsoleService.ShowLoading();

            var playerCharacter = _menuService.GetChosenCharacter();
            var opponentCharacter = _menuService.GetChosenOpponent();

            GameController gameController = new(playerCharacter, opponentCharacter);

            gameController.BeginNewGame();

            while (status == Status.InGame)
            {
                gameController.BeginNewRound();

                var playerCards = gameController.GetPlayerCards();
                var npcCards = gameController.GetNpcCards();

                var playerChosenCard = _consoleService.GetPlayerChosenCard(playerCards);
                var npcChosenCard = gameController.GetNpcChosenCard();
                ConsoleService.ShowChosenCards(playerChosenCard, npcChosenCard);
                PlayerInfoContainer playerInfoContainer = new(playerCards, playerChosenCard);
                PlayerInfoContainer npcInfoContainer = new(npcCards, npcChosenCard);
                Result result = gameController.JudgeRound(playerInfoContainer, npcInfoContainer);

                var card = result switch
                {
                    Result.BasicWin => _consoleService.GetPlayerWinCard(npcInfoContainer.Cards),
                    Result.BasicLose => gameController.GetNpcWinCard(),
                    Result.CharacterRuleWin => (Card)npcChosenCard,
                    Result.CharacterRuleLose => (Card)playerChosenCard,
                    _ => Card.None
                };

                gameController.ProcessSettlement(result, card);
                ConsoleService.ShowRoundResult(result, card);

                status = gameController.EndRound();
            }

            var outcome = GameController.GetOutcome();

            ConsoleService.Show(outcome);

            return status;
        }
    }

    public class Res
    {
        public string Token { get; set; } = string.Empty;
    }
}
