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

        public abstract void BuildGameLogic();

        public abstract Character GetCharacter();
    }
}
