namespace BoardGameClient.Menu;

internal class MenuDirector(IMenuBuilder builder)
{
    public void ConstructMenu()
    {
        var title = Resources.MainMenu;
        var mainMenuList = builder.BuildMainMenuList(title);

        title = Resources.ChooseCharacter;
        var characterMenuList = builder.BuildCharacterMenuList(title);

        builder.ConnectMenuOptionWithMenuList(mainMenuList.Options.First(), characterMenuList);

        title = Resources.ChooseOppoCharacter;
        var opponentMenuList = builder.BuildOpponentMenuList(title);

        var characterInfoMenuLists = characterMenuList.Options.Select(option => option.NextMenuList).ToList();
        var characterInfoMenuOptionList = characterInfoMenuLists.Select(menuList => menuList != null ? menuList.Options : new()).ToList();
        foreach (var option in from menuOptionList in characterInfoMenuOptionList where menuOptionList.Count > 0 select menuOptionList.First())
        {
            builder.ConnectMenuOptionWithMenuList(option, opponentMenuList);
        }
    }
}
