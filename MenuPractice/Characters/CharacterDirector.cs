using Menu_Practice.Characters.Builders;

namespace Menu_Practice.Characters
{
    internal class CharacterDirector
    {
        private readonly ICharacterBuilder _builder;
        public CharacterDirector(ref ICharacterBuilder builder)
        {
            _builder = builder;
        }

        public void ConstructChracter()
        {
            _builder.BuildName();
            _builder.BuildRule();
            _builder.BuildCards();
            _builder.BuildDisqualificationCondition();
            _builder.BuildEvolutionCondition();
            _builder.BuildAdditionalPointCondition();
            _builder.BuildGameLogic();
        }
    }
}
