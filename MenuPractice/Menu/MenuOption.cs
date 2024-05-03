namespace Menu_Practice
{
    internal class MenuOption
    {
        private readonly string _optionName;

        private readonly MenuList? _prevMenuList;

        private MenuList? _nextMenuList;

        public string OptionName { get => _optionName; }

        public MenuList? PrevMenuList { get => _prevMenuList; }

        public MenuList? NextMenuList { get => _nextMenuList; set => _nextMenuList = value; }

        public MenuOption()
        {
            _optionName = string.Empty;
        }

        public MenuOption(string optionName, MenuList? prevMenuList = null, MenuList? nextMenuList = null)
        {
            _optionName = optionName;
            _prevMenuList = prevMenuList;
            _nextMenuList = nextMenuList;
        }
    }
}
