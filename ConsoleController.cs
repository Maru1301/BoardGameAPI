using Menu_Practice.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    if (!((OpponentMenu)menuList).Options[i].Selected)
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
    }
}
