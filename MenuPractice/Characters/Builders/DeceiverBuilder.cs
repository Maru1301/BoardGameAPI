using static Menu_Practice.Program;

namespace Menu_Practice.Characters.Builders
{
    internal class DeceiverBuilder : ICharacterBuilder
    {
        private readonly Character _character = new();

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
            List<int> cards = [4, 1, 4];
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

        public Character GetCharacter()
        {
            return _character;
        }
    }
}
