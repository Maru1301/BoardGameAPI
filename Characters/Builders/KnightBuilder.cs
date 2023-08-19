using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu_Practice.Characters.Builders
{
    internal class KnightBuilder : ICharacterBuilder
    {
        private readonly Character _character;
        public KnightBuilder()
        {
            _character = new Character();
        }

        public void BuildName()
        {
            _character.SetName("騎士");
        }

        public void BuildRule()
        {
            _character.SetRule("騎士精神");
        }

        public void BuildCards()
        {
            int[] cards = { 3, 5, 1 };
            _character.SetCards(cards);
        }
        public void BuildDisqualificationCondition()
        {
            _character.SetDisqualificationCondition("皇冠數 - 匕首數 < 1");
        }

        public void BuildEvolutionCondition()
        {
            _character.SetEvolutionCondition("匕首數  = 0");
        }

        public void BuildAdditionalPointCondition()
        {
            _character.SetAdditionalPointCondition("皇冠數");
        }

        public Character GetCharacter()
        {
            return _character;
        }
    }
}
