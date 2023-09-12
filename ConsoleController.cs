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
        private string _hint;

        public ConsoleController()
        {
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

            do
            {
                ShowPlayerCards(playerCards);

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

            } while (IsChosenCardMoreThanOne(playerCards, key) && key != ConsoleKey.Enter);

            return _chooser;
        }

        private bool IsChosenCardMoreThanOne(List<int> playerCards, ConsoleKey key)
        {
            if (key == ConsoleKey.Enter && playerCards[_chooser] == 0)
            {
                _hint = "所選卡片剩餘0張，請選別張卡";
                return false;
            }

            return true;
        }

        private void ShowPlayerCards(List<int> playerCards)
        {
            Console.Clear();

            Console.WriteLine("選擇你要出的卡");
            foreach (var item in playerCards.Select((cardAmount, index) => new { index, cardAmount }))
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

        }
    }
}
