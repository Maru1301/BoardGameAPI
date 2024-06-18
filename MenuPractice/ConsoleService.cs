using Menu_Practice.Menu;

namespace Menu_Practice
{
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

        private static void ShowMenuList(MenuList menuList, int chooser)
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
                    Console.WriteLine(chooser == i
                        ? $"=>  {menuList.Options[i].OptionName}"
                        : $"    {menuList.Options[i].OptionName}");
                }
            }
            else
            {
                for (var i = 0; i < menuList.Options.Count; i++)
                {
                    Console.WriteLine(chooser == i
                        ? $"=>  {menuList.Options[i].OptionName}"
                        : $"    {menuList.Options[i].OptionName}");
                }
            }
        }

        public static MenuOption GetMenuOption(this MenuList menuList)
        {
            var chooser = 0;
            ConsoleKey key;

            do
            {
                ShowMenuList(menuList, chooser);

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
                        if (chooser < menuList.Options.Count - 1)
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
            }while (key != ConsoleKey.Enter);

            return menuList.Options[chooser];
        }

        public static Card GetChosenCard(this List<int> playerCards)
        {
            var chooser = 0;
            ConsoleKey key;

            var title = "請選擇你要出的卡";
            string hint = string.Empty;
            bool result;
            do
            {
                ShowCards(playerCards, title, hint, chooser);

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

        private static (bool , string)IsChosenCardEqualZero(List<int> playerCards, ConsoleKey key, int chooser)
        {
            if (key != ConsoleKey.Enter || playerCards[chooser] != 0) return (false, string.Empty);
            var hint = "所選卡片剩餘0張，請選別張卡";
            return (true, hint);
        }

        private static void ShowCards(IEnumerable<int> cards, string title, string hint, int chooser)
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
            var chooser = 0;
            ConsoleKey key;

            var title = "請選擇你要取得的卡";
            var hint = string.Empty;
            bool result;
            do
            {
                ShowCards(npcCards, title, hint, chooser);

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
                        if (chooser < npcCards.Count - 1)
                        {
                            chooser++;
                        }

                        break;
                    }
                }

                (result, hint) = IsChosenCardEqualZero(npcCards, key, chooser);
            } while (result || key != ConsoleKey.Enter);

            return (Card)chooser;
        }

        public static void ShowChosenCards(this (Card playerChosenCard, Card npcChosenCard) set)
        {
            var playerCardName = GetCardName(set.playerChosenCard);
            var npcCardName = GetCardName(set.npcChosenCard);
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
            else if(set.result == Result.BasicWin || set.result == Result.CharacterRuleWin)
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

        public static void Show(string text)
        {
            Console.WriteLine(text);
        }
    }
}
