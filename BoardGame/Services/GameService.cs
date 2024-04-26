using BoardGame.Infrastractures;
using BoardGame.Services.Interfaces;
using static BoardGame.Models.ViewModels.GameVMs;

namespace BoardGame.Services
{
    public class GameService : IService, IGameService
    {
        public Func<PlayerInfoVM, PlayerInfoVM, Result> MapRule(Character character)
            => character switch
            {
                Character.Assassin => Rule.AssassinRule,
                Character.Deceiver => Rule.DeceiverRule,
                Character.Knight => Rule.KnightRule,
                Character.Lobbyist => Rule.LobbyistRule,
                Character.Lord => Rule.LordRule,
                Character.Soldier => Rule.SoldierRule,
                _ => throw new NotImplementedException(),
            };

        public static class Rule
        {
            public static Result AssassinRule(PlayerInfoVM player1Info, PlayerInfoVM player2Info)
            {
                return (player1Info.CurrentChosenCard, player2Info.CurrentChosenCard) switch
                {
                    (Card.Crown, Card.Crown) or (Card.Sheild, Card.Sheild) => Result.Draw,
                    (Card.Crown, Card.Sheild) or (Card.Sheild, Card.Dagger) or (Card.Dagger, Card.Crown) => Result.BasicWin,
                    (Card.Crown, Card.Dagger) or (Card.Sheild, Card.Crown) or (Card.Dagger, Card.Sheild) => Result.BasicLose,
                    (Card.Dagger, Card.Dagger) => player1Info.CardSet.Dagger > player2Info.CardSet.Dagger && player1Info.CardSet.Dagger > 2 ? Result.CharacterRuleWin :
                                            player1Info.CardSet.Dagger < player2Info.CardSet.Dagger && player2Info.CardSet.Dagger > 2 ? Result.CharacterRuleLose : Result.Draw,
                    _ => throw new Exception("Invalid card combination"),
                };
            }

            public static Result DeceiverRule(PlayerInfoVM player1Info, PlayerInfoVM player2Info)
            {
                return (player1Info.CurrentChosenCard, player2Info.CurrentChosenCard) switch
                {
                    (Card.Crown, Card.Crown) => player1Info.CardSet.Sheild > player2Info.CardSet.Sheild ? Result.CharacterRuleWin :
                                            player1Info.CardSet.Sheild < player2Info.CardSet.Sheild ? Result.CharacterRuleLose : Result.Draw,
                    (Card.Crown, Card.Sheild) or (Card.Sheild, Card.Dagger) or (Card.Dagger, Card.Crown) => Result.BasicWin,
                    (Card.Crown, Card.Dagger) or (Card.Sheild, Card.Crown) or (Card.Dagger, Card.Sheild) => Result.BasicLose,
                    (Card.Sheild, Card.Sheild) or (Card.Dagger, Card.Dagger) => player1Info.CardSet.Sheild < player2Info.CardSet.Sheild ? Result.CharacterRuleWin :
                                            player1Info.CardSet.Sheild > player2Info.CardSet.Sheild ? Result.CharacterRuleLose : Result.Draw,
                    _ => throw new Exception("Invalid card combination"),
                };
            }

            public static Result KnightRule(PlayerInfoVM player1Info, PlayerInfoVM player2Info)
            {
                return (player1Info.CurrentChosenCard, player2Info.CurrentChosenCard) switch
                {
                    (Card.Crown, Card.Crown) => player1Info.CardSet.Dagger < player2Info.CardSet.Dagger ? Result.CharacterRuleWin :
                                            player1Info.CardSet.Dagger > player2Info.CardSet.Dagger ? Result.CharacterRuleLose : Result.Draw,
                    (Card.Crown, Card.Sheild) or (Card.Sheild, Card.Dagger) => Result.BasicWin,
                    (Card.Crown, Card.Dagger) => player1Info.CardSet.Dagger > player2Info.CardSet.Dagger ? Result.Draw : Result.BasicLose,
                    (Card.Sheild, Card.Crown) or (Card.Dagger, Card.Sheild) => Result.BasicLose,
                    (Card.Sheild, Card.Sheild) => player1Info.CardSet.Dagger < player2Info.CardSet.Dagger ? Result.CharacterRuleWin :
                                            player1Info.CardSet.Dagger == player2Info.CardSet.Dagger ? Result.Draw : Result.CharacterRuleLose,
                    (Card.Dagger, Card.Crown) => player1Info.CardSet.Dagger < player2Info.CardSet.Dagger ? Result.Draw : Result.BasicWin,
                    (Card.Dagger, Card.Dagger) => player1Info.CardSet.Dagger > player2Info.CardSet.Dagger ? Result.CharacterRuleWin :
                                            player1Info.CardSet.Dagger < player2Info.CardSet.Dagger ? Result.CharacterRuleLose : Result.Draw,
                    _ => throw new Exception("Invalid card combination"),
                };
            }

            public static Result LobbyistRule(PlayerInfoVM player1Info, PlayerInfoVM player2Info)
            {
                return (player1Info.CurrentChosenCard, player2Info.CurrentChosenCard) switch
                {
                    (Card.Crown, Card.Crown) => (player1Info.CardSet.Crown > 2 && player1Info.CardSet.Crown > player2Info.CardSet.Crown) ? Result.CharacterRuleWin :
                                            (player2Info.CardSet.Crown > 2 && player2Info.CardSet.Crown > player1Info.CardSet.Crown) ? Result.CharacterRuleLose : Result.Draw,
                    (Card.Crown, Card.Sheild) or (Card.Sheild, Card.Dagger) or (Card.Dagger, Card.Crown) => Result.BasicWin,
                    (Card.Crown, Card.Dagger) or (Card.Sheild, Card.Crown) or (Card.Dagger, Card.Sheild) => Result.BasicLose,
                    (Card.Sheild, Card.Sheild) => (player1Info.CardSet.Crown > player2Info.CardSet.Crown) ? Result.CharacterRuleWin :
                                        (player1Info.CardSet.Crown == player2Info.CardSet.Crown) ? Result.Draw : Result.CharacterRuleLose,
                    (Card.Dagger, Card.Dagger) => Result.Draw,
                    _ => throw new Exception("Invalid card combination"),
                };
            }

            public static Result LordRule(PlayerInfoVM player1Info, PlayerInfoVM player2Info)
            {
                return (player1Info.CurrentChosenCard, player2Info.CurrentChosenCard) switch
                {
                    (Card.Crown, Card.Crown) => (player1Info.CardSet.Crown > player2Info.CardSet.Crown) ? Result.CharacterRuleWin :
                                        (player1Info.CardSet.Crown == player2Info.CardSet.Crown) ? Result.Draw : Result.CharacterRuleLose,
                    (Card.Crown, Card.Sheild) or (Card.Sheild, Card.Dagger) or (Card.Dagger, Card.Crown) => Result.BasicWin,
                    (Card.Crown, Card.Dagger) or (Card.Dagger, Card.Sheild) => Result.BasicLose,
                    (Card.Sheild, Card.Crown) => (player1Info.CardSet.Crown == 0 || player2Info.CardSet.Crown == 0) ? Result.Draw : Result.BasicLose,
                    (Card.Sheild, Card.Sheild) => (player1Info.CardSet.Sheild > player2Info.CardSet.Sheild) ? Result.CharacterRuleWin :
                                        (player1Info.CardSet.Sheild == player2Info.CardSet.Sheild) ? Result.Draw : Result.CharacterRuleLose,
                    (Card.Dagger, Card.Dagger) => (player1Info.CardSet.Crown < player2Info.CardSet.Crown) ? Result.CharacterRuleWin :
                                        (player1Info.CardSet.Crown == player2Info.CardSet.Crown) ? Result.Draw : Result.CharacterRuleLose,
                    _ => throw new Exception("Invalid card combination"),
                };
            }

            public static Result SoldierRule(PlayerInfoVM player1Info, PlayerInfoVM player2Info)
            {
                return (player1Info.CurrentChosenCard, player2Info.CurrentChosenCard) switch
                {
                    (Card.Crown, Card.Crown) => (player1Info.CardSet.Sheild > player2Info.CardSet.Sheild) ? Result.CharacterRuleWin :
                                        (player1Info.CardSet.Sheild < player2Info.CardSet.Sheild) ? Result.CharacterRuleLose : Result.Draw,
                    (Card.Crown, Card.Sheild) or (Card.Sheild, Card.Dagger) or (Card.Dagger, Card.Crown) => Result.BasicWin,
                    (Card.Crown, Card.Dagger) or (Card.Sheild, Card.Crown) or (Card.Dagger, Card.Sheild) => Result.BasicLose,
                    (Card.Sheild, Card.Sheild) => (player1Info.CardSet.Sheild > player2Info.CardSet.Sheild) ? Result.CharacterRuleWin :
                                        (player1Info.CardSet.Sheild < player2Info.CardSet.Sheild) ? Result.CharacterRuleLose : Result.Draw,
                    (Card.Dagger, Card.Dagger) => (player1Info.CardSet.Sheild < player2Info.CardSet.Sheild) ? Result.CharacterRuleWin :
                                        (player1Info.CardSet.Sheild > player2Info.CardSet.Sheild) ? Result.CharacterRuleLose : Result.Draw,
                    _ => throw new Exception("Invalid card combination"),
                };
            }
        }
    }
}
