using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Menu_Practice.Characters;

namespace Menu_Practice.Menu
{
    internal class CharacterInfoMenu : MenuList
    {
        private readonly Character _character = new();

        public Character Character { get => _character; }

        public CharacterInfoMenu(Character character)
        {
            _character = character;
        }

        public override void ShowInfo()
        {
            string info = string.Empty;
            info += $"{_character.Name}\r\n使用規則: [{_character.Rule}]\r\n手牌: (皇冠 X {_character.Cards[0]}, 盾牌 X {_character.Cards[1]}, 匕首 X {_character.Cards[2]})\r\n失格條件: {_character.DisqualificationCondition}\r\n晉升條件: {_character.EvolutionCondition}\r\n額外分數: {_character.AdditionalPointCondition}";
            Console.WriteLine(info);
        }
    }
}
