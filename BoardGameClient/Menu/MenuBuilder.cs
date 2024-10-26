using BoardGameClient.Characters;

namespace BoardGameClient.Menu;

internal class MenuBuilder(CharacterList characterList) : IMenuBuilder
{
    private MenuList _rootMenuList = new();

    public MenuList BuildMainMenuList(string title)
    {
        const bool isRootList = true;
        var menuList = new MenuList(title, isRootList);
        MenuOption menuOptionStart = new("Start");
        menuList.Push(menuOptionStart);
        ConstructMenuList(menuList);
        _rootMenuList = menuList;

        return menuList;
    }

    public MenuList BuildCharacterMenuList(string title)
    {
        MenuList characterMenuList = new(title);

        foreach (var (name, character) in characterList.Characters)
        {
            MenuOption currentMenuOption = new(name);
            characterMenuList.Push(currentMenuOption);
            MenuList characterInfoMenu = new CharacterInfoMenu(character);
            ConstructMenuList(characterInfoMenu);
            currentMenuOption.NextMenuList = characterInfoMenu;
        }

        ConstructMenuList(characterMenuList);

        return characterMenuList;
    }

    public MenuList BuildOpponentMenuList(string title)
    {
        OpponentMenu opponentMenuList = new(title);
        foreach (var (name, character) in characterList.Characters)
        {
            var opponentMenuOption = new OpponentMenuOption(name, character);
            opponentMenuList.Push(opponentMenuOption);
        }

        ConstructMenuList(opponentMenuList);

        return opponentMenuList;
    }

    private static void ConstructMenuList(MenuList list)
    {
        if (list.IsRootList)
        {
            MenuOption exitOption = new("Exit");
            list.Push(exitOption);
        }
        else
        {
            if (list.GetType() == new CharacterInfoMenu(new Character()).GetType())
            {
                MenuOption goOption = new("Select");
                list.Push(goOption);
            }

            MenuOption backOption = new("Back");
            list.Push(backOption);
        }
    }

    public MenuList GetRootMenuList()
    {
        return _rootMenuList;
    }

    public void ConnectMenuOptionWithMenuList(MenuOption menuOption, MenuList menuList)
    {
        menuOption.NextMenuList = menuList;
    }
}
