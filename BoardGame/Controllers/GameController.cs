using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BoardGame.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        [HttpGet("[action]")]
        public NewGameInfo BeginNewGame(CharacterSet player, CharacterSet? computer = null)
        {
            //determine who goes first
            var random = new Random().Next(1);
            var whoGoesFirst = (WhoGoesFirst)random;
            return new NewGameInfo()
            {
                WhoGoesFirst = whoGoesFirst,
            };
        }

        private static Func<Card, Card, Result> MapRule(Character character) 
            => character switch
            {
                Character.Assassin => Rule.AssassinRule,
                Character.Deceiver => () => Result.BasicLose,
                Character.Knight => () => Result.CharacterRuleWin,
                Character.Lobbyist => () => Result.CharacterRuleLose,
                Character.Lord => () => Result.Draw,
                Character.Soldier => () => Result.Draw,
                _ => () => Result.Draw,
            };
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
    public class NewGameInfo
    {
        public WhoGoesFirst WhoGoesFirst { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    public static class Rule
    {
        public static Result AssassinRule(Card player1Card, Card player2Card)
        {
            switch ((player1Card, player2Card))
            {
                case (Card.Sheild, Card.Sheild):
                    return Result.Draw;
                case (Card.Sheild, Card.Crown):
                    return Result.BasicWin;
                case (Card.Sheild, Card.Dagger):
                    return Result.BasicLose;
                case (Card.Crown, Card.Sheild):
                    return Result.BasicLose;
                case (Card.Crown, Card.Crown):
                    return Result.Draw;
                case (Card.Crown, Card.Dagger):
                    return Result.BasicWin;
                case (Card.Dagger, Card.Sheild):
                    return Result.BasicWin;
                case (Card.Dagger, Card.Crown):
                    return Result.BasicLose;
                case (Card.Dagger, Card.Dagger):
                    if (player1Info.Cards[2] > player2Info.Cards[2] && player1Info.Cards[2] > 2)
                    {
                        return Result.CharacterRuleWin;
                    }
                    else if (player1Info.Cards[2] < player2Info.Cards[2] && player2Info.Cards[2] > 2)
                    {
                        return Result.CharacterRuleLose;
                    }

                    return Result.Draw;
            }

            return Result.Draw;
        }
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

    public class CharacterSet
    {
        public Character Character1 { get; set; }

        public Character Character2 { get; set; }

        public Character Characetr3 { get; set; }
    }

    public enum Card
    {
        Sheild,
        Crown,
        Dagger
    }

    public class CardSet
    {
        public Card Shield { get; set; }

        public Card Crown { get; set; }

        public Card Dagger { get; set; }
    }
}
