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
        private MenuList? _prevList;

        public List<MenuOption> Options { get => _options; }

        public MenuList()
        {
            this._options = new();
            _prevList = null;
        }

        public void Push(MenuOption option)
        {
            _options.Add(option);
        }

        public void AddParent(MenuList parentList)
        {
            _prevList = parentList;
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
