using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu_Practice.Characters.Builders
{
    internal interface ICharacterBuilder
    {
        public abstract void BuildName();

        public abstract void BuildRule();

        public abstract void BuildCards();

        public abstract void BuildDisqualificationCondition();

        public abstract void BuildEvolutionCondition();

        public abstract void BuildAdditionalPointCondition();

        public abstract Character GetCharacter();
    }

    enum Result
    {
        BasicWin = 0,
        BasicLose = 1,
        CharacterRuleWin = 2,
        CharacterRuleLose = 3,
        Draw = 4
    }
}
