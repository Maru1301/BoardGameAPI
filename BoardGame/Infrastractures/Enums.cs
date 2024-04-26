namespace BoardGame.Infrastractures
{
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string Member = "Member";
        public const string Guest = "Guest";
    }

    public enum Result : short
    {
        BasicWin = 1,
        BasicLose = -1,
        CharacterRuleWin = 2,
        CharacterRuleLose = -2,
        Draw = 0
    }

    public enum WhoGoesFirst
    {
        Player,
        Computer
    }

    public enum Character
    {
        Assassin,
        Deceiver,
        Knight,
        Lobbyist,
        Lord,
        Soldier
    }

    public enum Card
    {
        Crown,
        Sheild,
        Dagger
    }

    public class CardSet
    {
        public int Crown { get; set; }

        public int Sheild { get; set; }

        public int Dagger { get; set; }
    }
}
