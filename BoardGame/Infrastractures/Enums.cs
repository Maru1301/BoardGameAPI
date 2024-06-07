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
        public const string InvalidAccountOrPassword = "Invalid Account or Password";
        public const string MemberNotExist = "Member doesn't exist!";
        public const string WrongConfirmationCode = "Wrong confirmation code!";
        public const string ErrorParsingJwt = "Error occured while parsing JWT";
        public const string AccountExist = "Account already exists";
        public const string NameExist = "Name already exists";
        public const string EmailExist = "Email already exists";
        public const string AccountNotMatch = "Login Account does not matched!";
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
