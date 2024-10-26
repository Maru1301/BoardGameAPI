namespace BoardGameClient.Characters.Builders;

internal class AssassinBuilder : ICharacterBuilder
{
    private readonly Character _character = new();

    public void BuildName()
    {
        _character.Name = "刺客";
    }

    public void BuildRule()
    {
        _character.Rule = "刺客之道";
    }

    public void BuildCards()
    {
        List<int> cards = [2, 2, 5];
        _character.Cards = cards;
    }
    public void BuildDisqualificationCondition()
    {
        _character.DisqualificationCondition = "匕首數 - 皇冠數 < 2";
    }

    public void BuildEvolutionCondition()
    {
        _character.EvolutionCondition = "匕首數 > 5";
    }

    public void BuildAdditionalPointCondition()
    {
        _character.AdditionalPointCondition = "匕首數 - 皇冠數 - 2";
    }

    public Character GetCharacter()
    {
        return _character;
    }
}
