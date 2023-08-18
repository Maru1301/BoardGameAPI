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
            if (menuList.IsRootList)
            {
                if(_menuLists.Where(list => list.IsRootList == true).Any())
                {
                    throw new Exception("Root List has existed");
                }
            }
            _menuLists.Add(menuList);
        }

        public MenuList GetRootMenuList()
        {
            foreach(MenuList menuList in _menuLists)
            {
                if (menuList.IsRootList)
                {
                    return menuList;
                }
            }

            return new();
        }
    }
}
