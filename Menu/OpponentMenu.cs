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
        public OpponentMenu(string title) : base(title)
        {
            
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
