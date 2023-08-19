using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu_Practice.Characters.Builders
{
    internal class SoldierBuilder : ICharacterBuilder
    {
        private readonly Character _character;
        public SoldierBuilder()
        {
            _character = new Character();
        }

        public void BuildName()
        {
            _character.SetName("士兵");
        }

        public void BuildRule()
        {
            _character.SetRule("人海戰術");
        }

        public void BuildCards()
        {
            int[] cards = { 2, 5, 2 };
            _character.SetCards(cards);
        }
        public void BuildDisqualificationCondition()
        {
            _character.SetDisqualificationCondition("盾牌數 - 匕首數 < 2");
        }

        public void BuildEvolutionCondition()
        {
            _character.SetEvolutionCondition("盾牌數 > 5");
        }

        public void BuildAdditionalPointCondition()
        {
            _character.SetAdditionalPointCondition("盾牌數 - 匕首數 - 2");
        }

        public Character GetCharacter()
        {
            return _character;
        }
    }
}
