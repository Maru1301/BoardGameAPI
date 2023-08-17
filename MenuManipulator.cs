using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu_Practice
{
    internal class MenuManipulator
    {
        private Menu _menu;
        private MenuList _currentList;
        private int _chooser;

        public MenuManipulator(Menu menu)
        {
            this._menu = menu;
            _currentList = this._menu.MenuLists.First();
            _chooser = 0;
        }

        public void ActivateMenu()
        {
            bool NeedtoChangeView = true;
            while (true)
            {
                if (NeedtoChangeView)
                {
                    Show();
                }
                NeedtoChangeView = ReadUserInput();
            }
        }

        private bool ReadUserInput()
        {
            bool changed = false;
            ConsoleKey key = Console.ReadKey().Key;
            if(key != ConsoleKey.Escape)
            {
                if(key == ConsoleKey.Enter)
                {
                    ChangeList();
                    changed = true;
                }
                else if(key == ConsoleKey.UpArrow)
                {
                    if(_chooser > 0)
                    {
                        _chooser--;
                        changed = true;
                    }
                }
                else if(key == ConsoleKey.DownArrow)
                {
                    if(_chooser < _currentList.Options.Count-1)
                    {
                        _chooser++;
                        changed = true;
                    }
                }
            }

            return changed;
        }

        private void ChangeList()
        {
            MenuOption CurrentOption = GetCurrentOption();
            if (_currentList.IsRootList)
            {
                if(CurrentOption.OptionName == "Exit")
                {
                    Environment.Exit(0);
                }
            }
            
            if (CurrentOption.OptionName == "Back")
            {
                _currentList = _currentList.PrevList;
            }
        }

        private MenuOption GetCurrentOption()
        {
            return _currentList.Options[_chooser];
        }

        private void Show()
        {
            Console.Clear();

            var list = _currentList;
            for(int i = 0; i < list.Options.Count; i++)
            {
                if(_chooser == i)
                {
                    Console.WriteLine($"=>  {list.Options[i].OptionName}");
                }
                else
                {
                    Console.WriteLine($"    {list.Options[i].OptionName}");
                }
            }
        }
    }
}
