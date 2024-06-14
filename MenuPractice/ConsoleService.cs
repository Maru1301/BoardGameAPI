using Menu_Practice.Menu;

namespace Menu_Practice
{
    public class ConsoleService
    {
        private static readonly CancellationTokenSource _cancellationTokenSource = new();
        private static readonly List<string> Loadings =
            [
                "Loading",
                "Loading.",
                "Loading..",
                "Loading..."
            ];

        private int _chooser;
        private string _title = string.Empty;
        private string _hint = string.Empty;

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

        private void ShowMenuList(MenuList menuList)
        {
            Console.Clear();

            if (menuList.GetType() == typeof(CharacterInfoMenu))
            {
                var info = menuList.GetInfo();
                Console.WriteLine(info);
            }

            Console.WriteLine(menuList.Title);

            if (menuList.GetType() == typeof(OpponentMenu))
            {
                for(var i = 0; i < menuList.Options.Count; i++)
                {
                    Console.WriteLine(_chooser == i
                        ? $"=>  {menuList.Options[i].OptionName}"
                        : $"    {menuList.Options[i].OptionName}");
                }
            }
            else
            {
                for (var i = 0; i < menuList.Options.Count; i++)
                {
                    Console.WriteLine(_chooser == i
                        ? $"=>  {menuList.Options[i].OptionName}"
                        : $"    {menuList.Options[i].OptionName}");
                }
            }
        }

        public MenuOption GetMenuOption(MenuList menuList)
        {
            _chooser = 0;

            ConsoleKey key;

            do
            {
                ShowMenuList(menuList);

                key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                    {
                        if (_chooser > 0)
                        {
                            _chooser--;
                        }

                        break;
                    }
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                    {
                        if (_chooser < menuList.Options.Count - 1)
                        {
                            _chooser++;
                        }

                        break;
                    }
                    case ConsoleKey.Enter:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }while (key != ConsoleKey.Enter);

            return menuList.Options[_chooser];
        }

        public int GetPlayerChosenCard(List<int> playerCards)
        {
            _chooser = 0;

            ConsoleKey key;

            _title = "請選擇你要出的卡";
            do
            {
                ShowCards(playerCards);

                key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                    {
                        if (_chooser > 0)
                        {
                            _chooser--;
                        }

                        break;
                    }
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                    {
                        if (_chooser < playerCards.Count - 1)
                        {
                            _chooser++;
                        }

                        break;
                    }
                    case ConsoleKey.Enter:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                } 

            } while (IsChosenCardEqualZero(playerCards, key) || key != ConsoleKey.Enter);

            return _chooser;
        }

        private bool IsChosenCardEqualZero(List<int> playerCards, ConsoleKey key)
        {
            if (key != ConsoleKey.Enter || playerCards[_chooser] != 0) return false;
            _hint = "所選卡片剩餘0張，請選別張卡";
            return true;
        }

        private void ShowCards(IEnumerable<int> cards)
        {
            Console.Clear();

            Console.WriteLine(_title);
            foreach (var item in cards.Select((cardAmount, index) => new { index, cardAmount }))
            {
                Console.Write(_chooser == item.index ? "=>  " : "    ");

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
            Console.WriteLine(_hint);
            _hint = string.Empty;
        }

        public Card GetPlayerWinCard(List<int> npcCards)
        {
            _chooser = 0;

            ConsoleKey key;

            _title = "請選擇你要取得的卡";
            do
            {
                ShowCards(npcCards);

                key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                    {
                        if (_chooser > 0)
                        {
                            _chooser--;
                        }

                        break;
                    }
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                    {
                        if (_chooser < npcCards.Count - 1)
                        {
                            _chooser++;
                        }

                        break;
                    }
                }

            } while (IsChosenCardEqualZero(npcCards, key) || key != ConsoleKey.Enter);

            return (Card)_chooser;
        }

        public static void ShowChosenCards(int playerChosenCard, int npcChosenCard)
        {
            var playerCardName = GetCardName(playerChosenCard);
            var npcCardName = GetCardName(npcChosenCard);
            const int millisecond = 200;

            for (var i = 0; i < 12; i++)
            {
                var t = (i % 4) switch
                {
                    0 => "|",
                    1 => "/",
                    2 => "-",
                    _ => "\\"
                };
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("玩家\t對上\t電腦");
                Console.WriteLine($"{playerCardName}\t{t}\t{npcCardName}");
                Thread.Sleep(millisecond);
            }
        }

        private static string GetCardName(int card)
        {
            return card switch
            {
                0 => "皇冠",
                1 => "盾牌",
                2 => "匕首",
                _ => "",
            };
        }

        public static void ShowRoundResult(Result result, Card card)
        {
            var cardName = card switch
            {
                Card.Crown => "皇冠",
                Card.Shield => "盾牌",
                Card.Dagger => "匕首",
                _ => string.Empty
            };

            if (result == Result.Draw)
            {
                Console.WriteLine("平手!");
            }
            else if((int)result % 2 == 0)
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

        public (string account, string password) GetUserCredentials()
        {
            Console.WriteLine("Enter your account:");
            var account = Console.ReadLine();

            Console.WriteLine("Enter your password:");
            var password = Console.ReadLine();

            return (account, password);
        }

        public static void Show(string text)
        {
            Console.WriteLine(text);
        }
    }
}
