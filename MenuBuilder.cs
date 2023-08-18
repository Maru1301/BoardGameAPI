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
                MenuOption menuOptionStart = new("Start");
                menuList.Push(menuOptionStart);
                BuildMenuList(ref menuList);

                _menu.Push(menuList);

                MenuList menuList2 = new();

                MenuOption menuOption1 = new("Lord");
                menuList2.Push(menuOption1);
                Character Lord = new();
                MenuList characterInfoMenu = new CharacterInfoMenu(Lord);
                BuildMenuList(ref characterInfoMenu);
                menuOption1.NextMenuList = characterInfoMenu;
                characterInfoMenu.AddParent(menuList2);

                MenuOption menuOption3 = new("Deceiver");
                menuList2.Push(menuOption3);
                Character Deceiver = new();
                MenuList characterInfoMenu2 = new CharacterInfoMenu(Deceiver);
                BuildMenuList(ref characterInfoMenu2);
                menuOption3.NextMenuList = characterInfoMenu2;
                characterInfoMenu2.AddParent(menuList2);

                menuList2.AddParent(menuList);
                BuildMenuList(ref menuList2);

                menuOptionStart.NextMenuList = menuList2;

                _menu.Push(menuList2);
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.ToString());
                Environment.Exit(1);
            }
        }

        private static void BuildMenuList(ref MenuList list)
        {
            if (list.IsRootList)
            {
                MenuOption ExitOption = new("Exit");
                list.Push(ExitOption);
            }
            else 
            {
                if (list.GetType() == new CharacterInfoMenu(new Character()).GetType())
                {
                    MenuOption GoOption = new("Go");
                    list.Push(GoOption);
                }

                MenuOption BackOption = new("Back");
                list.Push(BackOption);
            }
        }

        public Menu BuildMenu()
        {
            return _menu;
        }
    }
}
