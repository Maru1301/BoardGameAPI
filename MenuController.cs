using Menu_Practice.Characters;
using Menu_Practice.Menu;
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
        private Character _chosenCharacter;
        private Character _chosenOpponent;

        public MenuController(MenuList rootMenuList)
        {
            _chosenCharacter = new();
            _chosenOpponent = new();  
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
        //public Status ActivateMenu()
        //{
        //    _status = Status.InMenu;
        //    bool NeedtoChangeView = true;
        //    while (_status == Status.InMenu)
        //    {
        //        if (NeedtoChangeView)
        //        {
        //            Show();
        //        }
        //        NeedtoChangeView = ReadUserInput();
        //    }

        //    return _status;
        //}

        //private bool ReadUserInput()
        //{
        //    bool changed = false;
        //    ConsoleKey key = Console.ReadKey().Key;
        //    try
        //    {
        //        if (key != ConsoleKey.Escape)
        //        {
        //            if (key == ConsoleKey.Enter)
        //            {
        //                ChangeList();
        //                _chooser = 0;
        //                changed = true;
        //            }
        //            else if (key == ConsoleKey.UpArrow)
        //            {
        //                if (_chooser > 0)
        //                {
        //                    _chooser--;
        //                    changed = true;
        //                }
        //            }
        //            else if (key == ConsoleKey.DownArrow)
        //            {
        //                var currentLIst = _menuStack.Peek();
        //                if (_chooser < currentLIst.Options.Count - 1)
        //                {
        //                    _chooser++;
        //                    changed = true;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //        Console.WriteLine("Press any key to go on...");
        //        Console.ReadKey();
        //        Console.Clear();
        //        changed = true;
        //    }

        //    return changed;
        //}

        //private void ChangeList()
        //{
        //    var currentList = _menuStack.Peek();
        //    MenuOption CurrentOption = GetCurrentOption();
        //    if (currentList.IsRootList)
        //    {
        //        if(CurrentOption.OptionName == "Exit")
        //        {
        //            _status = Status.End;
        //            return;
        //        }
        //    }
            
        //    if (CurrentOption.OptionName == "Back")
        //    {
        //        _menuStack.Pop();
        //    }
        //    else
        //    {
        //        if (CurrentOption.NextMenuList == null)
        //        {
        //            _status = Status.InGame;
        //            return;
        //        }

        //        _menuStack.Push(CurrentOption.NextMenuList);
        //    }
        //}

        public (Character character, Character opponent) GetChosenCharacterAndChosenOpponent()
        {
            return (_chosenCharacter, _chosenOpponent);
        }

        //private MenuOption GetCurrentOption()
        //{
        //    return _menuStack.Peek().Options[_chooser];
        //}

        //private void Show()
        //{
        //    Console.Clear();
        //    var currentList = _menuStack.Peek();
        //    if (currentList.GetType() == typeof(CharacterInfoMenu))
        //    {
        //        currentList.ShowInfo();
        //        _chosenCharacter = ((CharacterInfoMenu)currentList).Character;
        //    }else if(currentList.GetType() == typeof(OpponentMenu))
        //    {
        //        _chosenOpponent = ((OpponentMenu)currentList).Options[_chooser].Character;
        //    }

        //    var list = currentList;
        //    Console.WriteLine(list.Title);
        //    for(int i = 0; i < list.Options.Count; i++)
        //    {
        //        if(_chooser == i)
        //        {
        //            Console.WriteLine($"=>  {list.Options[i].OptionName}");
        //        }
        //        else
        //        {
        //            Console.WriteLine($"    {list.Options[i].OptionName}");
        //        }
        //    }
        //}
    }
}
