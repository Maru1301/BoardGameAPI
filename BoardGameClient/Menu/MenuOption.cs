namespace BoardGameClient.Menu;

public class MenuOption(string? optionName = null, MenuList? nextMenuList = null)
{
    private readonly string? _optionName = optionName;

    private MenuList? _nextMenuList = nextMenuList;

    public string? OptionName { get => _optionName; }

    public MenuList? NextMenuList { get => _nextMenuList; set => _nextMenuList = value; }
}
