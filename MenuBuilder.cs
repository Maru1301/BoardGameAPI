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
        private MenuList _rootMenuList;
        private readonly CharacterList _characterList;
        public MenuBuilder()
        {
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
                _rootMenuList = menuList;

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
                }

                BuildMenuList(ref characterMenuList);

                menuOptionStart.NextMenuList = characterMenuList;

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
                    MenuOption GoOption = new("Select");
                    list.Push(GoOption);
                }

                MenuOption BackOption = new("Back");
                list.Push(BackOption);
            }
        }

        public MenuList GetRootMenuList()
        {
            return _rootMenuList;
        }
    }
}
