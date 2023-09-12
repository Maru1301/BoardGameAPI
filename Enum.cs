namespace Menu_Practice
{
    internal partial class Program
    {
        public enum Status : short
        {
            InMenu = 1,
            InGame = 2,
            End = -1,
        }

        public enum Result : short
        {
            BasicWin = 0,
            BasicLose = 1,
            CharacterRuleWin = 2,
            CharacterRuleLose = 3,
            Draw = -1
        }

        public enum Card : short
        {
            None = -1,
            Crown = 0,
            Shield = 1,
            Dagger = 2
        }
    }
}