using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu_Practice
{
    internal class CharacterInfoMenu : MenuList
    {
        private readonly Character _character = new();

        public Character Character { get => _character; }

        public CharacterInfoMenu(Character character)
        {
            _character = character;
        }
    }
}
