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
        private int _chooser;

        public MenuManipulator(Menu menu)
        {
            this._menu = menu;
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
                    if(_chooser < _menu.GetCurrentList().Options.Count-1)
                    {
                        _chooser++;
                        changed = true;
                    }
                }
            }

            return changed;
        }

        private void Show()
        {
            Console.Clear();

            var list = _menu.GetCurrentList();
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
