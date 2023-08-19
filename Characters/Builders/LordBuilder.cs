using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu_Practice.Characters.Builders
{
    class LordBuilder : ICharacterBuilder
    {
        private readonly Character _character;
        public LordBuilder()
        {
            _character = new Character();
        }

        public void BuildName()
        {
            _character.Name = "領主";
        }

        public void BuildRule()
        {
            _character.Rule = "軍閥割據";
        }

        public void BuildCards()
        {
            int[] cards = { 0, 6, 3 };
            _character.Cards = cards;
        }
        public void BuildDisqualificationCondition()
        {
            _character.DisqualificationCondition = "盾牌數 - 皇冠數 < 5";
        }

        public void BuildEvolutionCondition()
        {
            _character.EvolutionCondition = "皇冠數 = 0";
        }

        public void BuildAdditionalPointCondition()
        {
            _character.AdditionalPointCondition = "盾牌數";
        }

        public Character GetCharacter()
        {
            return _character;
        }
    }
}
