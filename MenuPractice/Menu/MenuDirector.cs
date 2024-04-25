namespace Menu_Practice.Menu
{
    internal class MenuDirector
    {
        private readonly IMenuBuilder _builder;
        public MenuDirector(IMenuBuilder builder)
        {
            _builder = builder;
        }

        public void ConstructMenu()
        {
            string Title = "MainMenu";
            MenuList MainMenuList = _builder.BuildMainMenuList(Title);

            Title = "Choose a character";
            MenuList CharacterMenuList = _builder.BuildCharacterMenuList(Title);
            
            _builder.ConnectMenuOptionWithMenuList(MainMenuList.Options.First(), CharacterMenuList);
            
            Title = "Choose an opponent";
            MenuList OpponentMenuList = _builder.BuildOpponentMenuList(Title);
            
            List<MenuList?> CharacterInfoMenuLists = CharacterMenuList.Options.Select(option => option.NextMenuList).ToList();
            List<List<MenuOption>> CharacterInfoMenuOptionList = CharacterInfoMenuLists.Select(menuList => menuList != null ? menuList.Options : new()).ToList();
            foreach (var MenuOptionList in CharacterInfoMenuOptionList)
            {
                if(MenuOptionList != null && MenuOptionList.Count > 0)
                {
                    var Option = MenuOptionList.First();
                    _builder.ConnectMenuOptionWithMenuList(Option, OpponentMenuList);
                }
            }
        }
    }
}
