using Menu_Practice;
using Menu_Practice.Characters;
using Menu_Practice.Menu;

namespace Menu_Practice.Menu
{
    internal class OpponentMenu : MenuList
    {
        public OpponentMenu(string title) : base(title)
        {

        }

        public void Push(OpponentMenuOption option)
        {
            MenuOptions.Add(option);
        }
    }

    class OpponentMenuOption : MenuOption
    {
        private readonly Character _character;

        public Character Character { get => _character; }

        public OpponentMenuOption(string optionName, Character character, MenuList? prevMenuList = null, MenuList? nextMenuList = null) 
            : base(optionName, prevMenuList, nextMenuList)
        {
            _character = character;
        }
    }
}

internal static class OpponentMenuExt
{
    public static (MenuOption, int) FilterChosenCharacter(this OpponentMenu menu, Character character)
    {
        for (int i = 0; i < menu.Options.Count; i++)
        {
            if (menu.Options[i].OptionName == character.Name)
            {
                MenuOption removeOption = menu.Options[i];
                menu.Options.Remove(menu.Options[i]);
                return (removeOption, i);
            }
        }

        return (new(), 0);
    }
}
