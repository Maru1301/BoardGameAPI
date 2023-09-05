using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu_Practice
{
    internal class MenuList
    {
        private readonly string _title;
        private readonly List<MenuOption> _options;
        private readonly bool _isRootList;
        private readonly bool _isLastMenuList;

        public string Title { get => _title; }

        public List<MenuOption> Options { get => _options; }

        public bool IsRootList { get => _isRootList; }

        public bool IsLastMenuList { get => _isLastMenuList; }

        public MenuList(string title = "" , bool isLastMenuList = false, bool isRootList = false)
        {
            _title = title;
            this._options = new();
            _isRootList = isRootList;
            _isLastMenuList = isLastMenuList;
        }

        public void Push(MenuOption option)
        {
            _options.Add(option);
        }

        public virtual string GetInfo()
        {
            return string.Empty;
        }
    }
}
