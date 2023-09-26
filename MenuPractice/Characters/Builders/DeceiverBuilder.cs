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
            _character.Name = "詐欺師*";
        }

        public void BuildRule()
        {
            _character.Rule = "誘敵戰術";
        }

        public void BuildCards()
        {
            List<int> cards = new(){ 4, 1, 4 };
            _character.Cards = cards;
        }
        public void BuildDisqualificationCondition()
        {
            _character.DisqualificationCondition = "匕首數 - 盾牌數 < 2";
        }

        public void BuildEvolutionCondition()
        {
            _character.EvolutionCondition = "盾牌數 = 0";
        }

        public void BuildAdditionalPointCondition()
        {
            _character.AdditionalPointCondition = "匕首數 - 2";
        }

        public void BuildGameLogic()
        {

        }

        public Character GetCharacter()
        {
            return _character;
        }
    }
}
