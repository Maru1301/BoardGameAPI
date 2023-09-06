using Menu_Practice.Characters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public void FilterOutOptions(Func<OpponentMenuOption, bool> func)
        {
            foreach(var option in _options)
            {
                option.Selected = false;
                if (func(option))
                {
                    option.Selected = true;
                }
            }
        }
    }

    class OpponentMenuOption : MenuOption
    {
        private readonly Character _character;
        private bool _selected;

        public Character Character { get => _character; }

        public bool Selected { get => _selected; set => _selected = value; }
        public OpponentMenuOption(string optionName, Character character, MenuList? prevMenuList = null, MenuList? nextMenuList = null) 
            : base(optionName, prevMenuList, nextMenuList)
        {
            _character = character;
        }
    }
}
