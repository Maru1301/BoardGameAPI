namespace BoardGameClient.Characters.Builders;

internal interface ICharacterBuilder
{
    public abstract void BuildName();

    public abstract void BuildRule();

    public abstract void BuildCards();

    public abstract void BuildDisqualificationCondition();

    public abstract void BuildEvolutionCondition();

    public abstract void BuildAdditionalPointCondition();

    public abstract Character GetCharacter();
}
