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
            _character.Name = "士兵";
        }

        public void BuildRule()
        {
            _character.Rule = "人海戰術";
        }

        public void BuildCards()
        {
            int[] cards = { 2, 5, 2 };
            _character.Cards = cards;
        }
        public void BuildDisqualificationCondition()
        {
            _character.DisqualificationCondition = "盾牌數 - 匕首數 < 2";
        }

        public void BuildEvolutionCondition()
        {
            _character.EvolutionCondition = "盾牌數 > 5";
        }

        public void BuildAdditionalPointCondition()
        {
            _character.AdditionalPointCondition = "盾牌數 - 匕首數 - 2";
        }

        public Character GetCharacter()
        {
            return _character;
        }
    }
}
