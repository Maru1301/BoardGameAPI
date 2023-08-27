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
        private readonly bool _isRootList;

        public List<MenuOption> Options { get => _options; }

        public bool IsRootList { get => _isRootList; }

        public MenuList(bool isRootList = false)
        {
            this._options = new();
            _isRootList = isRootList;
        }

        public void Push(MenuOption option)
        {
            _options.Add(option);
        }

        public virtual void ShowInfo()
        {

        }
    }
}
