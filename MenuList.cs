using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu_Practice
{
    internal class MenuList
    {
        private readonly List<MenuOption> _options;

        public MenuList()
        {
            this._options = new();
        }

        public void Push(MenuOption option)
        {
            _options.Add(option);
        }

        public void Show()
        {
            foreach (MenuOption option in _options)
            {
                Console.WriteLine(option.OptionName);
            }
        }
    }
}
