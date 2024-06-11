namespace Menu_Practice.Menu
{
    internal class MenuList
    {
        protected List<MenuOption> MenuOptions;

        public string Title { get; }

        public List<MenuOption> Options { get => MenuOptions; set => MenuOptions = value; }

        public bool IsRootList { get; }

        public MenuList(string title = "", bool isRootList = false)
        {
            Title = title;
            MenuOptions = new List<MenuOption>();
            IsRootList = isRootList;
        }

        public void Push(MenuOption option)
        {
            MenuOptions.Add(option);
        }

        public void Insert(int index, MenuOption option)
        {
            MenuOptions.Insert(index, option);
        }

        public virtual string GetInfo()
        {
            return string.Empty;
        }
    }
}
