using Menu_Practice.Characters;
using Menu_Practice.Menu;

namespace Menu_Practice
{
    public class MenuService
    {
        private readonly Stack<MenuList> _menuStack = new();
        private Character _chosenCharacter = new();
        private Character _chosenOpponent = new();

        public MenuList CurrentMenuList { get => _menuStack.Peek(); }

        public void Init(MenuList rootMenuList)
        {
            _menuStack.Push(rootMenuList);
        }

        public MenuList GetCurrentMenuList()
        {
            return _menuStack.Peek();
        }

        public bool IsCurrentMenuListRoot()
        {
            return _menuStack.Peek().IsRootList;
        }

        public void MoveToNextMenuList(MenuOption menuOption)
        {
            if (menuOption.NextMenuList != null)
            {
                _menuStack.Push(menuOption.NextMenuList);
            }
        }

        public MenuList MoveToPrevMenuList()
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
