using Menu_Practice.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Menu_Practice.Program;

namespace Menu_Practice
{
    internal class MenuController
    {
        private readonly Stack<MenuList> _menuStack = new();
        private int _chooser;
        private Character _chosenCharacter;
        private Character _chosenOpponent;
        private bool _gameStart;

        public MenuController(MenuList rootMenuList)
        {
            _menuStack.Push(rootMenuList);
            _chooser = 0;
            _gameStart = false;
        }

        public void ActivateMenu()
        {
            bool NeedtoChangeView = true;
            while (_gameStart != true)
            {
                if (NeedtoChangeView)
                {
                    Show();
                }
                NeedtoChangeView = ReadUserInput();
            }
        }

        private bool ReadUserInput()
        {
            bool changed = false;
            ConsoleKey key = Console.ReadKey().Key;
            try
            {
                if (key != ConsoleKey.Escape)
                {
                    if (key == ConsoleKey.Enter)
                    {
                        ChangeList();
                        _chooser = 0;
                        changed = true;
                    }
                    else if (key == ConsoleKey.UpArrow)
                    {
                        if (_chooser > 0)
                        {
                            _chooser--;
                            changed = true;
                        }
                    }
                    else if (key == ConsoleKey.DownArrow)
                    {
                        var currentLIst = _menuStack.Peek();
                        if (_chooser < currentLIst.Options.Count - 1)
                        {
                            _chooser++;
                            changed = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Press any key to go on...");
                Console.ReadKey();
                Console.Clear();
                changed = true;
            }

            return changed;
        }

        private void ChangeList()
        {
            var currentList = _menuStack.Peek();
            MenuOption CurrentOption = GetCurrentOption();
            if (currentList.IsRootList)
            {
                if(CurrentOption.OptionName == "Exit")
                {
                    Environment.Exit(0);
                }
            }
            
            if (CurrentOption.OptionName == "Back")
            {
                _menuStack.Pop();
            }
            else
            {
                if (CurrentOption.NextMenuList == null)
                {
                    _gameStart = true;
                    return;
                }

                _menuStack.Push(CurrentOption.NextMenuList);
            }
        }

        public (Character character, Character opponent) GetChosenCharacterAndChosenOpponent()
        {
            return (_chosenCharacter, _chosenOpponent);
        }

        private MenuOption GetCurrentOption()
        {
            return _menuStack.Peek().Options[_chooser];
        }

        private void Show()
        {
            Console.Clear();
            var currentList = _menuStack.Peek();
            if (currentList.GetType() == typeof(CharacterInfoMenu))
            {
                currentList.ShowInfo();
                _chosenCharacter = ((CharacterInfoMenu)currentList).Character;
            }

            var list = currentList;
            for(int i = 0; i < list.Options.Count; i++)
            {
                if(_chooser == i)
                {
                    Console.WriteLine($"=>  {list.Options[i].OptionName}");
                }
                else
                {
                    Console.WriteLine($"    {list.Options[i].OptionName}");
                }
            }
        }
    }
}
