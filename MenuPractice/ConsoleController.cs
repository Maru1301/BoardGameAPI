using Menu_Practice.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Menu_Practice.Program;

namespace Menu_Practice
{
    internal class ConsoleController
    {
        private static readonly List<string> Loadings = new()
            {
                "Loading",
                "Loading.",
                "Loading..",
                "Loading..."
            };

        private int _chooser;
        private string _title;
        private string _hint;

        public ConsoleController()
        {
            _title = string.Empty;
            _hint = string.Empty;
            _chooser = 0;
        }

        public void ShowLoading()
        {
            int x = 0;

            int time = 12;
            int millisec = 100;
            while (x < time)
            {
                Console.Clear();
                Console.WriteLine(Loadings[x % Loadings.Count]);
                Thread.Sleep(millisec);
                x++;
            }
        }

        private void ShowMenuList(MenuList menuList)
        {
            Console.Clear();

            if (menuList.GetType() == typeof(CharacterInfoMenu))
            {
                string info = menuList.GetInfo();
                Console.WriteLine(info);
            }

            Console.WriteLine(menuList.Title);

            if (menuList.GetType() == typeof(OpponentMenu))
            {
                for(int i = 0; i < menuList.Options.Count; i++)
                {
                    if (_chooser == i)
                    {
                        Console.WriteLine($"=>  {menuList.Options[i].OptionName}");
                    }
                    else
                    {
                        Console.WriteLine($"    {menuList.Options[i].OptionName}");
                    }
                }
            }
            else
            {
                for (int i = 0; i < menuList.Options.Count; i++)
                {
                    if (_chooser == i)
                    {
                        Console.WriteLine($"=>  {menuList.Options[i].OptionName}");
                    }
                    else
                    {
                        Console.WriteLine($"    {menuList.Options[i].OptionName}");
                    }
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

                if (key == ConsoleKey.UpArrow || key == ConsoleKey.W)
                {
                    if (_chooser > 0)
                    {
                        _chooser--;
                    }
                }
                else if (key == ConsoleKey.DownArrow || key == ConsoleKey.S)
                {
                    if (_chooser < menuList.Options.Count - 1)
                    {
                        _chooser++;
                    }
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

                if (key == ConsoleKey.UpArrow || key == ConsoleKey.W)
                {
                    if (_chooser > 0)
                    {
                        _chooser--;
                    }
                }
                else if (key == ConsoleKey.DownArrow || key == ConsoleKey.S)
                {
                    if (_chooser < playerCards.Count - 1)
                    {
                        _chooser++;
                    }
                } 

            } while (IsChosenCardEqualZero(playerCards, key) || key != ConsoleKey.Enter);

            return _chooser;
        }

        private bool IsChosenCardEqualZero(List<int> playerCards, ConsoleKey key)
        {
            if (key == ConsoleKey.Enter && playerCards[_chooser] == 0)
            {
                _hint = "所選卡片剩餘0張，請選別張卡";
                return true;
            }

            return false;
        }

        private void ShowCards(List<int> cards)
        {
            Console.Clear();

            Console.WriteLine(_title);
            foreach (var item in cards.Select((cardAmount, index) => new { index, cardAmount }))
            {
                if (_chooser == item.index)
                {
                    Console.Write("=>  ");
                }
                else
                {
                    Console.Write("    ");
                }

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

                if (key == ConsoleKey.UpArrow || key == ConsoleKey.W)
                {
                    if (_chooser > 0)
                    {
                        _chooser--;
                    }
                }
                else if (key == ConsoleKey.DownArrow || key == ConsoleKey.S)
                {
                    if (_chooser < npcCards.Count - 1)
                    {
                        _chooser++;
                    }
                }

            } while (IsChosenCardEqualZero(npcCards, key) || key != ConsoleKey.Enter);

            return (Card)_chooser;
        }

        public void ShowChosenCards(int playerChosenCard, int npcChosenCard)
        {
            string playerCardName = GetCardName(playerChosenCard);
            string npcCardName = GetCardName(npcChosenCard);
            string t;
            int millisec = 200;

            for (int i = 0; i < 12; i++)
            {
                if(i % 4 == 0)
                {
                    t = "|";
                }
                else if(i % 4 == 1)
                {
                    t = "/";
                }
                else if(i % 4 == 2)
                {
                    t = "-";
                }
                else
                {
                    t = "\\";
                }
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("玩家\t對上\t電腦");
                Console.WriteLine($"{playerCardName}\t{t}\t{npcCardName}");
                Thread.Sleep(millisec);
            }
        }

        private string GetCardName(int card)
        {
            return card switch
            {
                0 => "皇冠",
                1 => "盾牌",
                2 => "匕首",
                _ => "",
            };
        }

        public void ShowRoundResult(Result result, Card card)
        {
            string cardName = string.Empty;
            switch (card)
            {
                case Card.Crown:
                    cardName = "皇冠";
                    break;
                case Card.Shield:
                    cardName = "盾牌";
                    break;
                case Card.Dagger:
                    cardName = "匕首";
                    break;
            }

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

        public void Show(string text)
        {
            Console.WriteLine(text);
        }
    }
}
