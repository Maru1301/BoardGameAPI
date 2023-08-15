using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu_Practice
{
    internal class MenuBuilder
    {
        private readonly Menu _menu;
        public MenuBuilder() 
        {
            _menu = new Menu();
        }

        public void ConstructMenu()
        {
            MenuList menuList = new();
            MenuOption menuOption = new("Start");
            menuList.Push(menuOption);

            MenuOption menuOption2 = new("Exit");
            menuList.Push(menuOption2);

            _menu.Push(menuList);

            MenuList menuList2 = new();
            MenuOption menuOption1 = new("Lord");
            menuList2.Push(menuOption1);

            MenuOption menuOption3 = new("Deceiver");
            menuList2.Push(menuOption3);
            menuList2.AddParent(menuList);

            _menu.Push(menuList2);
        }

        public Menu BuildMenu()
        {
            try
            {
                _menu.Initialize();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return _menu;
        }
    }
}
