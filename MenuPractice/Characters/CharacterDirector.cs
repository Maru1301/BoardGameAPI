using Menu_Practice.Characters.Builders;

namespace Menu_Practice.Characters
{
    internal class CharacterDirector(ref ICharacterBuilder builder)
    {
        private readonly ICharacterBuilder _builder = builder;

        public void ConstructCharacter()
        {
            _builder.BuildName();
            _builder.BuildRule();
            _builder.BuildCards();
            _builder.BuildDisqualificationCondition();
            _builder.BuildEvolutionCondition();
            _builder.BuildAdditionalPointCondition();
        }
    }
}
