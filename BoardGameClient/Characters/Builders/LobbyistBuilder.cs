namespace BoardGameClient.Characters.Builders;

internal class LobbyistBuilder : ICharacterBuilder
{
    private readonly Character _character = new();

    public void BuildName()
    {
        _character.Name = "說客";
    }

    public void BuildRule()
    {
        _character.Rule = "外交手腕";
    }

    public void BuildCards()
    {
        List<int> cards = [5, 2, 2];
        _character.Cards = cards;
    }
    public void BuildDisqualificationCondition()
    {
        _character.DisqualificationCondition = "皇冠數 - 盾牌數 < 2";
    }

    public void BuildEvolutionCondition()
    {
        _character.EvolutionCondition = "皇冠數 > 5";
    }

    public void BuildAdditionalPointCondition()
    {
        _character.AdditionalPointCondition = "盾牌數 + 1";
    }

    public Character GetCharacter()
    {
        return _character;
    }
}
