using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Menu_Practice.Characters;

namespace Menu_Practice
{
    internal class MenuBuilder
    {
        private readonly Menu _menu;
        private readonly CharacterList _characterList;
        public MenuBuilder()
        {
            _menu = new Menu();
            _characterList = new CharacterList();
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

                MenuList characterMenuList = new();

                MenuOption currentMenuOption;
                Character currentCharacter;
                MenuList currentCharacterInfoMenu;
                foreach ((string name, Character character) in _characterList.Characters)
                {
                    currentMenuOption = new(name);
                    characterMenuList.Push(currentMenuOption);
                    currentCharacter = character;
                    currentCharacterInfoMenu = new CharacterInfoMenu(character);
                    BuildMenuList(ref currentCharacterInfoMenu);
                    currentMenuOption.NextMenuList = currentCharacterInfoMenu;
                    currentCharacterInfoMenu.AddParent(characterMenuList);
                }

                characterMenuList.AddParent(menuList);
                BuildMenuList(ref characterMenuList);

                menuOptionStart.NextMenuList = characterMenuList;

                _menu.Push(characterMenuList);
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
