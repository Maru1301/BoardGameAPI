using Menu_Practice;
using Menu_Practice.Characters;
using Menu_Practice.Menu;

namespace Menu_Practice.Menu
{
    public class OpponentMenu(string title) : MenuList(title)
    {
        public void Push(OpponentMenuOption option)
        {
            MenuOptions.Add(option);
        }
    }

    public class OpponentMenuOption(string optionName, Character character, MenuList? nextMenuList = null) : MenuOption(optionName, nextMenuList)
    {
        private readonly Character _character = character;

        public Character Character { get => _character; }
    }
}

public static class OpponentMenuExt
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
