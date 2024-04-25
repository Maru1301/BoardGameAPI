using Menu_Practice.Characters;

namespace Menu_Practice.Menu
{
    internal class MenuBuilder : IMenuBuilder
    {
        private MenuList _rootMenuList;
        private readonly CharacterList _characterList;
        public MenuBuilder()
        {
            _rootMenuList = new MenuList();
            _characterList = new CharacterList();
        }

        public MenuList BuildMainMenuList(string title)
        {
            MenuList menuList;
            bool IsRootList = true;
            menuList = new(title, IsRootList);
            MenuOption menuOptionStart = new("Start");
            menuList.Push(menuOptionStart);
            ConstructMenuList(menuList);
            _rootMenuList = menuList;

            return menuList;
        }

        public MenuList BuildCharacterMenuList(string title)
        {
            MenuOption CurrentMenuOption;
            Character CurrentCharacter;
            MenuList CharacterMenuList = new(title);
            MenuList CharacterInfoMenu;

            foreach ((string name, Character character) in _characterList.Characters)
            {
                CurrentMenuOption = new(name);
                CharacterMenuList.Push(CurrentMenuOption);
                CurrentCharacter = character;
                CharacterInfoMenu = new CharacterInfoMenu(character);
                ConstructMenuList(CharacterInfoMenu);
                CurrentMenuOption.NextMenuList = CharacterInfoMenu;
            }

            ConstructMenuList(CharacterMenuList);

            return CharacterMenuList;
        }

        public MenuList BuildOpponentMenuList(string title)
        {
            OpponentMenuOption opponentMenuOption;
            OpponentMenu opponentMenuList = new(title);
            foreach((string name, Character character) in _characterList.Characters)
            {
                opponentMenuOption = new(name, character);
                opponentMenuList.Push(opponentMenuOption);
            }

            ConstructMenuList((MenuList)opponentMenuList);

            return opponentMenuList;
        }

        public static void ConstructMenuList(MenuList list)
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

        public void ConnectMenuOptionWithMenuList( MenuOption menuOption, MenuList menuList)
        {
            menuOption.NextMenuList = menuList;
        }

        
    }
}
