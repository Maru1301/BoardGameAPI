namespace Menu_Practice.Characters.Builders
{
    internal class KnightBuilder : ICharacterBuilder
    {
        private readonly Character _character = new();

        public void BuildName()
        {
            _character.Name = "騎士";
        }

        public void BuildRule()
        {
            _character.Rule = "騎士精神";
        }

        public void BuildCards()
        {
            List<int> cards = [3, 5, 1];
            _character.Cards = cards;
        }
        public void BuildDisqualificationCondition()
        {
            _character.DisqualificationCondition = "皇冠數 - 匕首數 < 1";
        }

        public void BuildEvolutionCondition()
        {
            _character.EvolutionCondition = "匕首數  = Card.Crown";
        }

        public void BuildAdditionalPointCondition()
        {
            _character.AdditionalPointCondition = "皇冠數";
        }

        public Character GetCharacter()
        {
            return _character;
        }
    }
}
