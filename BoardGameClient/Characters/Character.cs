namespace BoardGameClient.Characters;

public class Character
{
    public string Name { get; set; } = string.Empty;

    public string Rule { get; set; } = string.Empty;

    public List<int> Cards { get; set; } = [];

    public string DisqualificationCondition { get; set; } = string.Empty;

    public string EvolutionCondition { get; set; } = string.Empty;

    public string AdditionalPointCondition { get; set; } = string.Empty;

    public Character()
    {

    }

    public Character(Character character)
    {
        Name = character.Name;
        Rule = character.Rule;
        Cards = [.. character.Cards];
        DisqualificationCondition = character.DisqualificationCondition;
        EvolutionCondition = character.EvolutionCondition;
        AdditionalPointCondition = character.AdditionalPointCondition;
    }
}
