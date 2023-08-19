using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu_Practice.Characters.Builders
{
    internal class LobbyistBuilder : ICharacterBuilder
    {
        private readonly Character _character;
        public LobbyistBuilder()
        {
            _character = new Character();
        }

        public void BuildName()
        {
            _character.SetName("說客");
        }

        public void BuildRule()
        {
            _character.SetRule("外交手腕");
        }

        public void BuildCards()
        {
            int[] cards = { 5, 2, 2 };
            _character.SetCards(cards);
        }
        public void BuildDisqualificationCondition()
        {
            _character.SetDisqualificationCondition("皇冠數 - 盾牌數 < 2");
        }

        public void BuildEvolutionCondition()
        {
            _character.SetEvolutionCondition("皇冠數 > 5");
        }

        public void BuildAdditionalPointCondition()
        {
            _character.SetAdditionalPointCondition("盾牌數 + 1");
        }

        public Character GetCharacter()
        {
            return _character;
        }
    }
}
