namespace BoardGameClient;

public enum Status : short
{
    InMenu,
    InGame,
    End,
}

public enum Result : short
{
    BasicWin,
    BasicLose,
    CharacterRuleWin,
    CharacterRuleLose,
    Draw
}

public enum Card
{
    Crown,
    Shield,
    Dagger,
    None
}