namespace BoardGame.Infrastractures
{
    public static class Role
    {
        public const string Admin = "Admin"; 
        public const string Member = "Member";
        public const string Guest = "Guest";
    }

    public static class ErrorCode
    {
        public static string InvalidAccountOrPassword { get => "Invalid Account or Password"; } 
        public static string MemberNotExist { get => "Member doesn't exist!"; }
        public static string WrongConfirmationCode { get => "Wrong confirmation code!"; }
        public static string ErrorParsingJwt { get => "Error occured while parsing JWT"; }
        public static string AccountExist { get => "Account already exists"; }
        public static string NameExist { get => "Name already exists"; }
        public static string EmailExist { get => "Email already exists"; }
        public static string AccountNotMatch { get => "Login Account does not matched!"; }
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
