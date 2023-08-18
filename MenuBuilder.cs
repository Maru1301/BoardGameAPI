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
            try
            {
                bool IsRootList = true;
                MenuList menuList = new(IsRootList);
                MenuOption menuOption = new("Start");
                menuList.Push(menuOption);
                BuildRootMenuList(ref menuList);

                _menu.Push(menuList);

                MenuList menuList2 = new();
                MenuOption menuOption1 = new("Lord");
                menuList2.Push(menuOption1);
                MenuOption menuOption3 = new("Deceiver");
                menuList2.Push(menuOption3);
                menuList2.AddParent(menuList);
                BuildMenuList(ref menuList2);

                menuOption.NextMenuList = menuList2;

                _menu.Push(menuList2);
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.ToString());
                Environment.Exit(1);
            }
        }

        private static void BuildRootMenuList(ref MenuList list)
        {
            MenuOption ExitOption = new("Exit");
            list.Push(ExitOption);
        }

        private static void BuildMenuList(ref MenuList list)
        {
            MenuOption BackOption = new("Back");
            list.Push(BackOption);
        }

        public Menu BuildMenu()
        {
            return _menu;
        }
    }
}
