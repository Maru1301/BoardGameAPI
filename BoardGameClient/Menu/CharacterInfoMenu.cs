using BoardGameClient.Characters;

namespace BoardGameClient.Menu;

internal class CharacterInfoMenu(Character character) : MenuList
{
    public Character Character { get; } = character;

    public override string GetInfo()
    {
        var info = string.Empty;
        info += $"{Character.Name}\r\n使用規則: {Character.Rule}\r\n手牌: (皇冠 X {Character.Cards[0]}, 盾牌 X {Character.Cards[1]}, 匕首 X {Character.Cards[2]})\r\n失格條件: {Character.DisqualificationCondition}\r\n晉升條件: {Character.EvolutionCondition}\r\n額外分數: {Character.AdditionalPointCondition}";
        return info;
    }
}
