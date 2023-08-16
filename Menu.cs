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

        private MenuList _currentList;
        public Menu()
        {
            _menuLists = new();
            _currentList = new MenuList();
        }

        public void Push(MenuList menuList)
        {
            _menuLists.Add(menuList);
        }

        public void Initialize() 
        {
            if( _menuLists.Count != 0 ) 
            { 
                _currentList = _menuLists.First();
            }
            else
            {
                throw new Exception("There is nothing in the Menu now!");
            }
        }

        public MenuList GetCurrentList()
        {
            return _currentList;
        }
    }
}
