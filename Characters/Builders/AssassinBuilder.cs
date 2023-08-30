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
            _character.Name = "刺客";
        }

        public void BuildRule()
        {
            _character.Rule = "刺客之道";
        }

        public void BuildCards()
        {
            List<int> cards = new(){ 2, 2, 5 };
            _character.Cards = cards;
        }
        public void BuildDisqualificationCondition()
        {
            _character.DisqualificationCondition = "匕首數 - 皇冠數 < 2";
        }

        public void BuildEvolutionCondition()
        {
            _character.EvolutionCondition = "匕首數 > 5";
        }

        public void BuildAdditionalPointCondition()
        {
            _character.AdditionalPointCondition = "匕首數 - 皇冠數 - 2";
        }

        public Character GetCharacter()
        {
            return _character;
        }
    }
}
