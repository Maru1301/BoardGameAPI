using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu_Practice.Characters.Builders
{
    internal class AssassinBuilder : ICharacterBuilder
    {
        private readonly Character _character;
        public AssassinBuilder()
        {
            _character = new Character();
        }

        public void BuildName()
        {
            _character.SetName("刺客");
        }

        public void BuildRule()
        {
            _character.SetRule("刺客之道");
        }

        public void BuildCards()
        {
            int[] cards = { 2, 2, 5 };
            _character.SetCards(cards);
        }
        public void BuildDisqualificationCondition()
        {
            _character.SetDisqualificationCondition("匕首數 - 皇冠數 < 2");
        }

        public void BuildEvolutionCondition()
        {
            _character.SetEvolutionCondition("匕首數 > 5");
        }

        public void BuildAdditionalPointCondition()
        {
            _character.SetAdditionalPointCondition("匕首數 - 皇冠數 - 2");
        }

        public Character GetCharacter()
        {
            return _character;
        }
    }
}
