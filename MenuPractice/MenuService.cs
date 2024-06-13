using Menu_Practice.Characters;
using Menu_Practice.Menu;

namespace Menu_Practice
{
    public class MenuService
    {
        private readonly Stack<MenuList> _menuStack = new();
        private Character _chosenCharacter = new();
        private Character _chosenOpponent = new();

        public void Init(MenuList rootMenuList)
        {
            _menuStack.Push(rootMenuList);
        }

        public MenuList GetNextMenuList(MenuOption menuOption)
        {
            MenuList menuList = new();

            if (menuOption.NextMenuList != null)
            {
                menuList = menuOption.NextMenuList;

                _menuStack.Push(menuList);
            }

            return menuList;
        }

        public MenuList GetPrevMenuList()
        {
            _menuStack.Pop();

            return _menuStack.Peek();
        }

        public void SetChosenCharacter(Character chosenCharacter)
        {
            _chosenCharacter = chosenCharacter;
        }

        public void SetChosenOpponent(Character chosenOpponent)
        {
            _chosenOpponent = chosenOpponent;
        }

        public Character GetChosenCharacter()
        {
            return _chosenCharacter;
        }

        public Character GetChosenOpponent()
        {
            return _chosenOpponent;
        }
    }
}
