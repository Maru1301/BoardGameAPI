using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu_Practice.Characters.Builders
{
    internal class DeceiverBuilder : ICharacterBuilder
    {
        private readonly Character _character;
        public DeceiverBuilder()
        {
            _character = new Character();
        }

        public void BuildName()
        {
            _character.SetName("詐欺師*");
        }

        public void BuildRule()
        {
            _character.SetRule("誘敵戰術");
        }

        public void BuildCards()
        {
            int[] cards = { 4, 1, 4 };
            _character.SetCards(cards);
        }
        public void BuildDisqualificationCondition()
        {
            _character.SetDisqualificationCondition("匕首數 - 盾牌數 < 2");
        }

        public void BuildEvolutionCondition()
        {
            _character.SetEvolutionCondition("盾牌數 = 0");
        }

        public void BuildAdditionalPointCondition()
        {
            _character.SetAdditionalPointCondition("匕首數 - 2");
        }

        public Character GetCharacter()
        {
            return _character;
        }
    }
}
