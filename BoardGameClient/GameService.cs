using Microsoft.AspNetCore.SignalR.Client;

namespace BoardGameClient;

public class GameService
{
    private readonly Player _player = new();
    private readonly Player _npc = new();
    private bool _playerGoFirst;
    private int _roundCount;
    private const int EndGame = 5;

    public GameService(Character character, Character opponent)
    {
        //_player.Character = new Character(character);
        //_npc.Character = new Character(opponent);
    }

    public async Task Test(string jwtToken)
    {
        Console.WriteLine("Game Client");

        // Replace with your actual hub URL
        string hubUrl = "https://localhost:44318/gameHub";

        HubConnection connection = new HubConnectionBuilder()
            .WithUrl(hubUrl, options =>
            {
                options.Headers.Add("Authorization", "Bearer " + jwtToken);
            })
            .WithAutomaticReconnect()
            .Build();

        // Handle receiving moves from the opponent
        connection.On<string>("Test", (gameId) =>
        {
            Console.WriteLine($"{gameId}");
        });

        //// Handle game start notification
        //connection.On<string>("GameStarted", (gameId) =>
        //{
        //    Console.WriteLine($"Game started! Your game ID is: {gameId}");
        //});

        try
        {
            await connection.StartAsync();
            Console.WriteLine("Connected to the game server");
            Console.WriteLine(connection.State);
            Console.WriteLine(connection.ConnectionId);
            Console.ReadLine();
            await connection.InvokeAsync("HostGame");
            Console.ReadLine();
            // Prompt for player name
            //Console.Write("Enter your name: ");
            //string playerName = Console.ReadLine();

            //// Join matchmaking
            //await connection.InvokeAsync("JoinMatchmaking", playerName);
            //Console.WriteLine("Joined matchmaking. Waiting for an opponent...");

            //while (true)
            //{
            //    Console.Write("Enter your move (or 'quit' to exit): ");
            //    string move = Console.ReadLine();

            //    if (move.ToLower() == "quit")
            //        break;

            //    // Send the move to the server
            //    await connection.InvokeAsync("SendMove", move);
            //}
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.ReadLine();
        }
        finally
        {
            await connection.StopAsync();
        }
    }

    public void BeginNewGame()
    {
        _playerGoFirst = IsPlayerGoFirst();
        _roundCount = 0;
    }

    private static bool IsPlayerGoFirst()
    {
        //who go first
        var random = new Random();
        var whoGoFirst = random.Next(1);
        const int playerGoFirst = 1;

        return whoGoFirst == playerGoFirst;
    }

    public void BeginNewRound()
    {

    }

    //public List<int> PlayerCards { get => _player.Character.Cards; set => _player.Character.Cards = value; }

    //public List<int> GetNpcCards { get => _npc.Character.Cards; set => _npc.Character.Cards = value; }

    //public (Card playerCard, Card npcCard) GetChosenCard()
    //{
    //    var playerChosenCard = PlayerCards.GetChosenCard();

    //    Random random = new();
    //    var npcCards = _npc.Character.Cards;

    //    var canChooseCards = (from item in npcCards.Select((cardAmount, index) => new { index, cardAmount }) where item.cardAmount > 0 select item.index).ToList();

    //    var chosenCard = canChooseCards[random.Next(canChooseCards.Count)];

    //    return (playerChosenCard, (Card)chosenCard);
    //}

    public Result JudgeRound()
    {
        //todo: call judge api
        return Result.BasicWin;
    }

    public Card GetPlayerWinCard()
    {
        return Card.None;
    }

    //public Card GetNpcWinCard()
    //{
    //    Random random = new();
    //    var playerCards = _player.Character.Cards;

    //    var canChooseCards = (from item in playerCards.Select((cardAmount, index) => new { index, cardAmount }) where item.cardAmount > 0 select item.index).ToList();

    //    var chosenCard = canChooseCards[random.Next(canChooseCards.Count)];

    //    return (Card)chosenCard;
    //}

    //public void ProcessSettlement(Result result, Card card)
    //{
    //    var playerWin = IsPlayerWin(result);

    //    switch (playerWin)
    //    {
    //        case null:
    //            return;
    //        case true:
    //            _player.Character.Cards[(int)card]++;
    //            _npc.Character.Cards[(int)card]--;
    //            break;
    //        default:
    //            _npc.Character.Cards[(int)card]++;
    //            _player.Character.Cards[(int)card]--;
    //            break;
    //    }
    //}

    private static bool? IsPlayerWin(Result result)
    {
        if (result.Equals(Result.Draw))
        {
            return null;
        }

        var resultNum = (int)result;
        return resultNum % 2 == 0;
    }

    public Status EndRound()
    {
        if (_roundCount == EndGame)
        {
            return Status.InMenu;
        }

        _playerGoFirst = !_playerGoFirst;

        return Status.InGame;
    }

    public string GetOutcome()
    {
        const int playerPoint = 0;
        const int npcPoint = 0;

        return playerPoint == npcPoint ? "平手" :
               playerPoint > npcPoint ? "勝利" :
               "敗北";
    }

    private class Player
    {
        public Character Character { get; set; } = new();

        public bool GoFirst { get; set; }
    }

    public static List<Character> GetCharacterList()
    {
        return [new Character() {
            Name = "111",
            Rule = "111",
            Card = [0,1,2],
            Disqualification = "111",
            Evolution = "111",
            AdditionalPoint = "111",
        }];
    }
}

public class PlayerInfoContainer(List<int> cards, Card chosenCard)
{
    public List<int> Cards { get => cards; }

    public Card ChosenCard { get => chosenCard; }
}
