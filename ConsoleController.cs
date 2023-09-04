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
        private MenuList _menuList;
        private int _chooser;
        public ConsoleController()
        {
            _menuList = new MenuList();
            _chooser = 0;
        }

        public void SetCurrentMenuList(MenuList menuList)
        {
            _menuList = menuList;
        }

        public void ShowMenuList()
        {
            Console.Clear();

            Console.WriteLine(_menuList.Title);
            for (int i = 0; i < _menuList.Options.Count; i++)
            {
                if (_chooser == i)
                {
                    Console.WriteLine($"=>  {_menuList.Options[i].OptionName}");
                }
                else
                {
                    Console.WriteLine($"    {_menuList.Options[i].OptionName}");
                }
            }
        }

        public MenuOption GetMenuOption()
        {
            ConsoleKey key;
            {
                key = Console.ReadKey().Key;

                if (key == ConsoleKey.UpArrow)
                {
                    if (_chooser > 0)
                    {
                        _chooser--;
                    }
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    if (_chooser < _menuList.Options.Count - 1)
                    {
                        _chooser++;
                    }
                }
            }
            while (key != ConsoleKey.Enter) ;

            return _menuList.Options[_chooser];
        }
    }
}
