namespace Menu_Practice
{
    internal class MenuList
    {
        private readonly string _title;
        protected List<MenuOption> _options;
        private readonly bool _isRootList;

        public string Title { get => _title; }

        public List<MenuOption> Options { get => _options; set => _options = value; }

        public bool IsRootList { get => _isRootList; }

        public MenuList(string title = "", bool isRootList = false)
        {
            _title = title;
            this._options = new();
            _isRootList = isRootList;
        }

        public void Push(MenuOption option)
        {
            _options.Add(option);
        }

        public void Insert(int index, MenuOption option)
        {
            _options.Insert(index, option);
        }

        public virtual string GetInfo()
        {
            return string.Empty;
        }
    }
}
