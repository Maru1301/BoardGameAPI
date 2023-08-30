using Menu_Practice.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu_Practice.Menu
{
    internal class OpponentMenu : MenuList
    {
        private List<OpponentMenuOption> _options;

        public new List<OpponentMenuOption> Options { get { return _options; } }

        public OpponentMenu()
        {
            _options = new();
        }

        public void Push(OpponentMenuOption option)
        {
            _options.Add(option);
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
