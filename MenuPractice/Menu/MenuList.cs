namespace Menu_Practice.Menu
{
    public class MenuList(string title = "", bool isRootList = false)
    {
        protected List<MenuOption> MenuOptions = [];

        public string Title { get; } = title;

        public List<MenuOption> Options { get => MenuOptions; set => MenuOptions = value; }

        public bool IsRootList { get; } = isRootList;

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
