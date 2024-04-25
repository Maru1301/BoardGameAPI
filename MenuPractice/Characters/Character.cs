using static Menu_Practice.Program;

namespace Menu_Practice.Characters
{
    internal class Character
    {
        private string _name = string.Empty;
        private string _rule = string.Empty;
        private List<int> _cards = new();
        private string _disqualificationCondition = string.Empty;
        private string _evolutionCondition = string.Empty;
        private string _additionalPointCondition = string.Empty;

        public string Name { get => _name; set => _name = value; }
        
        public string Rule { get => _rule; set => _rule = value; }
        
        public List<int> Cards { get => _cards; set => _cards = value; }
        
        public string DisqualificationCondition { get => _disqualificationCondition; set => _disqualificationCondition = value; }
        
        public string EvolutionCondition { get => _evolutionCondition; set => _evolutionCondition = value; }
        
        public string AdditionalPointCondition { get => _additionalPointCondition; set => _additionalPointCondition = value; }

        public Func<PlayerInfoContainer, PlayerInfoContainer, Result> UseRuleLogic { get; set; }

        public Character()
        {
        }

        public Character(Character character)
        {
            this._name = character.Name;
            this._rule = character.Rule;
            this._cards = new(character.Cards);
            this._disqualificationCondition = character.DisqualificationCondition;
            this._evolutionCondition = character.EvolutionCondition;
            this.AdditionalPointCondition = character.AdditionalPointCondition;
            this.UseRuleLogic = character.UseRuleLogic;
        }
    }
}
