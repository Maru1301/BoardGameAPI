namespace BoardGameClient;

public static class ConsoleService
{
    private static readonly CancellationTokenSource _cancellationTokenSource = new();
    private static readonly List<string> Loadings =
        [
            "Loading",
            "Loading.",
            "Loading..",
            "Loading..."
        ];

    public static async Task ShowLoading()
    {
        var x = 0;
        const int millisecond = 100;
        var cancellationToken = _cancellationTokenSource.Token;

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Console.Clear();
                Console.WriteLine(Loadings[x % Loadings.Count]);
                await Task.Delay(millisecond, cancellationToken);
                x++;
            }
        }
        catch (TaskCanceledException)
        {
            // Expected exception when the task is cancelled
        }
    }

    public static void StopShowLoading()
    {
        _cancellationTokenSource.Cancel();
    }

    public static void RevealCard()
    {
        // Define the card characters
        var cards = new List<char[,]>
        {
            new char[,]
            {
                {'┌', '─', '─', '─', '─', '─', '─', '─', '┐'},
                {'│', '0', ' ', ' ', ' ', ' ', ' ', ' ', '│'},
                {'│', ' ', ' ', ' ', '0', ' ', ' ', ' ', '│'},
                {'│', ' ', ' ', ' ', ' ', ' ', ' ', '0', '│'},
                {'└', '─', '─', '─', '─', '─', '─', '─', '┘'}
            },
            new char[,]
            {
                {' ', '┌', '─', '─', '─', '─', '─', '┐', ' '},
                {' ', '│', '0', ' ', ' ', ' ', ' ', '│', ' '},
                {' ', '│', ' ', ' ', '0', ' ', ' ', '│', ' '},
                {' ', '│', ' ', ' ', ' ', ' ', '0', '│', ' '},
                {' ', '└', '─', '─', '─', '─', '─', '┘', ' '}
            },
            new char[,]
            {
                {' ', ' ', '┌', '─', '─', '─', '┐', ' ', ' '},
                {' ', ' ', '│', '0', ' ', ' ', '│', ' ', ' '},
                {' ', ' ', '│', ' ', '0', ' ', '│', ' ', ' '},
                {' ', ' ', '│', ' ', ' ', '0', '│', ' ', ' '},
                {' ', ' ', '└', '─', '─', '─', '┘', ' ', ' '}
            },
            new char[,]
            {
                {' ', ' ', ' ', '┌', '─', '┐', ' ', ' ', ' '},
                {' ', ' ', ' ', '│', '0', '│', ' ', ' ', ' '},
                {' ', ' ', ' ', '│', '0', '│', ' ', ' ', ' '},
                {' ', ' ', ' ', '│', '0', '│', ' ', ' ', ' '},
                {' ', ' ', ' ', '└', '─', '┘', ' ', ' ', ' '}
            },
            new char[,]
            {
                {' ', ' ', ' ', ' ', '│', ' ', ' ', ' ', ' '},
                {' ', ' ', ' ', ' ', '│', ' ', ' ', ' ', ' '},
                {' ', ' ', ' ', ' ', '│', ' ', ' ', ' ', ' '},
                {' ', ' ', ' ', ' ', '│', ' ', ' ', ' ', ' '},
                {' ', ' ', ' ', ' ', '│', ' ', ' ', ' ', ' '}
            },
            new char[,]
            {
                {' ', ' ', ' ', '┌', '─', '┐', ' ', ' ', ' '},
                {' ', ' ', ' ', '│', ' ', '│', ' ', ' ', ' '},
                {' ', ' ', ' ', '│', ' ', '│', ' ', ' ', ' '},
                {' ', ' ', ' ', '│', ' ', '│', ' ', ' ', ' '},
                {' ', ' ', ' ', '└', '─', '┘', ' ', ' ', ' '}
            },
            new char[,]
            {
                {' ', ' ', '┌', '─', '─', '─', '┐', ' ', ' '},
                {' ', ' ', '│', ' ', ' ', ' ', '│', ' ', ' '},
                {' ', ' ', '│', ' ', ' ', ' ', '│', ' ', ' '},
                {' ', ' ', '│', ' ', ' ', ' ', '│', ' ', ' '},
                {' ', ' ', '└', '─', '─', '─', '┘', ' ', ' '}
            },
            new char[,]
            {
                {' ', '┌', '─', '─', '─', '─', '─', '┐', ' '},
                {' ', '│', ' ', ' ', ' ', ' ', ' ', '│', ' '},
                {' ', '│', ' ', ' ', ' ', ' ', ' ', '│', ' '},
                {' ', '│', ' ', ' ', ' ', ' ', ' ', '│', ' '},
                {' ', '└', '─', '─', '─', '─', '─', '┘', ' '}
            },
            new char[,]
            {
                {'┌', '─', '─', '─', '─', '─', '─', '─', '┐'},
                {'│', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '│'},
                {'│', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '│'},
                {'│', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '│'},
                {'└', '─', '─', '─', '─', '─', '─', '─', '┘'}
            }
        };

        // Animate the card flip
        for (int i = 0; i < cards.Count; i++)
        {
            Console.Clear();
            DrawCard(cards[i]);
            Thread.Sleep(60); // Adjust the delay to control the animation speed
        }

        Console.ReadLine(); // Wait for user input before closing
    }

    private static void DrawCard(char[,] card)
    {
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 9; x++)
            {
                Console.Write(card[y, x]);
            }
            Console.WriteLine();
        }
    }

    public static Card GetChosenCard(this List<int> playerCards)
    {
        return Choose(playerCards, Resources.ChooseCard, string.Empty);
    }

    private static Card Choose(List<int> playerCards, string title, string hint)
    {
        var chooser = 0;
        ConsoleKey key;
        bool result;
        do
        {
            playerCards.ShowCards(title, hint, chooser);

            key = Console.ReadKey().Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    {
                        if (chooser > 0)
                        {
                            chooser--;
                        }

                        break;
                    }
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    {
                        if (chooser < playerCards.Count - 1)
                        {
                            chooser++;
                        }

                        break;
                    }
                case ConsoleKey.Enter:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            (result, hint) = IsChosenCardEqualZero(playerCards, key, chooser);
        } while (result || key != ConsoleKey.Enter);

        return (Card)chooser;
    }

    private static (bool, string) IsChosenCardEqualZero(List<int> playerCards, ConsoleKey key, int chooser)
    {
        if (key != ConsoleKey.Enter || playerCards[chooser] != 0) return (false, string.Empty);
        var hint = Resources.ChooseOtherCard;
        return (true, hint);
    }

    private static void ShowCards(this IEnumerable<int> cards, string title, string hint, int chooser)
    {
        Console.Clear();

        Console.WriteLine(title);
        foreach (var item in cards.Select((cardAmount, index) => new { index, cardAmount }))
        {
            Console.Write(chooser == item.index ? "=>  " : "    ");

            switch (item.index)
            {
                case 0:
                    Console.WriteLine($"皇冠x{item.cardAmount}");
                    break;
                case 1:
                    Console.WriteLine($"盾牌x{item.cardAmount}");
                    break;
                case 2:
                    Console.WriteLine($"匕首x{item.cardAmount}");
                    break;
            }
        }
        Console.WriteLine(hint);
    }

    public static Card GetPlayerWinCard(this List<int> npcCards)
    {
        return Choose(npcCards, Resources.ChooseObtainCard, string.Empty);
    }

    public static void ShowChosenCards(this (Card playerChosenCard, Card npcChosenCard) set)
    {
        var playerCardName = GetCardName(set.playerChosenCard);
        var npcCardName = GetCardName(set.npcChosenCard);
        const int millisecond = 200;

        for (var i = 0; i < 12; i++)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("玩家\t\t\t電腦");
            Console.WriteLine($"{playerCardName}\t\t\t{npcCardName}");
            Thread.Sleep(millisecond);
        }
    }

    private static string GetCardName(Card card)
    {
        return card switch
        {
            Card.Crown => "皇冠",
            Card.Shield => "盾牌",
            Card.Dagger => "匕首",
            _ => "",
        };
    }

    public static void ShowRoundResult(this (Result result, Card card) set)
    {
        var cardName = GetCardName(set.card);

        if (set.result == Result.Draw)
        {
            Console.WriteLine("平手!");
        }
        else if (set.result == Result.BasicWin || set.result == Result.CharacterRuleWin)
        {
            Console.WriteLine($"勝利!\r\n取得一張{cardName}");
        }
        else
        {
            Console.WriteLine($"失敗!\r\n失去一張{cardName}");
        }
        Console.WriteLine("按下Enter繼續");
        ConsoleKey key;
        do
        {
            key = Console.ReadKey().Key;
        } while (key != ConsoleKey.Enter);
    }

    public static (string? account, string? password) GetUserCredentials()
    {
        Console.WriteLine("Enter your account:");
        var account = Console.ReadLine();

        Console.WriteLine("Enter your password:");
        var password = Console.ReadLine();

        return (account, password);
    }

    public static void Show(this string text)
    {
        Console.WriteLine(text);
    }
}
