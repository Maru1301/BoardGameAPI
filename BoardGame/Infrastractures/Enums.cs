namespace BoardGame.Infrastractures
{
    public static class Role
    {
        public const string Admin = "Admin";
        public const string Member = "Member";
        public const string Guest = "Guest";
    }

    public enum Result : short
    {
        Player1Win = 1,
        Player2Win = -1,
        Player1CharacterRuleWin = 2,
        Player2CharacterRuleWin = -2,
        Draw = 0
    }

    public enum WhoGoesFirst
    {
        Player1,
        Player2
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
