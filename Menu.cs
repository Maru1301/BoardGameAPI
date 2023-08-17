using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu_Practice
{
    internal class Menu
    {
        private List<MenuList> _menuLists;

        public List<MenuList> MenuLists { get => _menuLists; }

        public Menu()
        {
            _menuLists = new();
        }

        public void Push(MenuList menuList)
        {
            _menuLists.Add(menuList);
        }
    }
}
