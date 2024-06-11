using static Menu_Practice.Program;

namespace Menu_Practice.Characters
{
    internal class Character
    {
        public string Name { get; set; } = string.Empty;

        public string Rule { get; set; } = string.Empty;

        public List<int> Cards { get; set; } = [];

        public string DisqualificationCondition { get; set; } = string.Empty;

        public string EvolutionCondition { get; set; } = string.Empty;

        public string AdditionalPointCondition { get; set; } = string.Empty;

        public Func<PlayerInfoContainer, PlayerInfoContainer, Result>? UseRuleLogic { get; set; }

        public Character()
        {
            
        }

        public Character(Character character)
        {
            Name = character.Name;
            Rule = character.Rule;
            Cards = [..character.Cards];
            DisqualificationCondition = character.DisqualificationCondition;
            EvolutionCondition = character.EvolutionCondition;
            AdditionalPointCondition = character.AdditionalPointCondition;
            UseRuleLogic = character.UseRuleLogic;
        }
    }
}
