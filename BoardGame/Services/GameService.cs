using BoardGame.Infrastractures;
using BoardGame.Services.Interfaces;
using static BoardGame.Models.DTOs.GameDTOs;
using static BoardGame.Models.ViewModels.GameVMs;

namespace BoardGame.Services
{
    public class GameService : IService, IGameService
    {
        public void BeginNewGame(GameInfoDTO dto)
        {
            ValidateGameInfo(dto);
            //determine who goes first
            var random = new Random().Next(1);
            var whoGoesFirst = (WhoGoesFirst)random;
        }

        private bool ValidateGameInfo(GameInfoDTO dto)
        {
            var characterNames = new HashSet<Character>
            {
                dto.Player1Characters.Character1,
                dto.Player1Characters.Character2,
                dto.Player1Characters.Characetr3,
                dto.Player2Characters.Character1,
                dto.Player2Characters.Character2,
                dto.Player2Characters.Characetr3
            };

            return characterNames.Count == 6;
        }

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
                    (Card.Crown, Card.Sheild) or (Card.Sheild, Card.Dagger) or (Card.Dagger, Card.Crown) => Result.Player1Win,
                    (Card.Crown, Card.Dagger) or (Card.Sheild, Card.Crown) or (Card.Dagger, Card.Sheild) => Result.Player2Win,
                    (Card.Dagger, Card.Dagger) => player1Info.CardSet.Dagger > player2Info.CardSet.Dagger && player1Info.CardSet.Dagger > 2 ? Result.Player1CharacterRuleWin :
                                            player1Info.CardSet.Dagger < player2Info.CardSet.Dagger && player2Info.CardSet.Dagger > 2 ? Result.Player2CharacterRuleWin : Result.Draw,
                    _ => throw new Exception("Invalid card combination"),
                };
            }

            public static Result DeceiverRule(PlayerInfoVM player1Info, PlayerInfoVM player2Info)
            {
                return (player1Info.CurrentChosenCard, player2Info.CurrentChosenCard) switch
                {
                    (Card.Crown, Card.Crown) => player1Info.CardSet.Sheild > player2Info.CardSet.Sheild ? Result.Player1CharacterRuleWin :
                                            player1Info.CardSet.Sheild < player2Info.CardSet.Sheild ? Result.Player2CharacterRuleWin : Result.Draw,
                    (Card.Crown, Card.Sheild) or (Card.Sheild, Card.Dagger) or (Card.Dagger, Card.Crown) => Result.Player1Win,
                    (Card.Crown, Card.Dagger) or (Card.Sheild, Card.Crown) or (Card.Dagger, Card.Sheild) => Result.Player2Win,
                    (Card.Sheild, Card.Sheild) or (Card.Dagger, Card.Dagger) => player1Info.CardSet.Sheild < player2Info.CardSet.Sheild ? Result.Player1CharacterRuleWin :
                                            player1Info.CardSet.Sheild > player2Info.CardSet.Sheild ? Result.Player2CharacterRuleWin : Result.Draw,
                    _ => throw new Exception("Invalid card combination"),
                };
            }

            public static Result KnightRule(PlayerInfoVM player1Info, PlayerInfoVM player2Info)
            {
                return (player1Info.CurrentChosenCard, player2Info.CurrentChosenCard) switch
                {
                    (Card.Crown, Card.Crown) => player1Info.CardSet.Dagger < player2Info.CardSet.Dagger ? Result.Player1CharacterRuleWin :
                                            player1Info.CardSet.Dagger > player2Info.CardSet.Dagger ? Result.Player2CharacterRuleWin : Result.Draw,
                    (Card.Crown, Card.Sheild) or (Card.Sheild, Card.Dagger) => Result.Player1Win,
                    (Card.Crown, Card.Dagger) => player1Info.CardSet.Dagger > player2Info.CardSet.Dagger ? Result.Draw : Result.Player2Win,
                    (Card.Sheild, Card.Crown) or (Card.Dagger, Card.Sheild) => Result.Player2Win,
                    (Card.Sheild, Card.Sheild) => player1Info.CardSet.Dagger < player2Info.CardSet.Dagger ? Result.Player1CharacterRuleWin :
                                            player1Info.CardSet.Dagger == player2Info.CardSet.Dagger ? Result.Draw : Result.Player2CharacterRuleWin,
                    (Card.Dagger, Card.Crown) => player1Info.CardSet.Dagger < player2Info.CardSet.Dagger ? Result.Draw : Result.Player1Win,
                    (Card.Dagger, Card.Dagger) => player1Info.CardSet.Dagger > player2Info.CardSet.Dagger ? Result.Player1CharacterRuleWin :
                                            player1Info.CardSet.Dagger < player2Info.CardSet.Dagger ? Result.Player2CharacterRuleWin : Result.Draw,
                    _ => throw new Exception("Invalid card combination"),
                };
            }

            public static Result LobbyistRule(PlayerInfoVM player1Info, PlayerInfoVM player2Info)
            {
                return (player1Info.CurrentChosenCard, player2Info.CurrentChosenCard) switch
                {
                    (Card.Crown, Card.Crown) => (player1Info.CardSet.Crown > 2 && player1Info.CardSet.Crown > player2Info.CardSet.Crown) ? Result.Player1CharacterRuleWin :
                                            (player2Info.CardSet.Crown > 2 && player2Info.CardSet.Crown > player1Info.CardSet.Crown) ? Result.Player2CharacterRuleWin : Result.Draw,
                    (Card.Crown, Card.Sheild) or (Card.Sheild, Card.Dagger) or (Card.Dagger, Card.Crown) => Result.Player1Win,
                    (Card.Crown, Card.Dagger) or (Card.Sheild, Card.Crown) or (Card.Dagger, Card.Sheild) => Result.Player2Win,
                    (Card.Sheild, Card.Sheild) => (player1Info.CardSet.Crown > player2Info.CardSet.Crown) ? Result.Player1CharacterRuleWin :
                                        (player1Info.CardSet.Crown == player2Info.CardSet.Crown) ? Result.Draw : Result.Player2CharacterRuleWin,
                    (Card.Dagger, Card.Dagger) => Result.Draw,
                    _ => throw new Exception("Invalid card combination"),
                };
            }

            public static Result LordRule(PlayerInfoVM player1Info, PlayerInfoVM player2Info)
            {
                return (player1Info.CurrentChosenCard, player2Info.CurrentChosenCard) switch
                {
                    (Card.Crown, Card.Crown) => (player1Info.CardSet.Crown > player2Info.CardSet.Crown) ? Result.Player1CharacterRuleWin :
                                        (player1Info.CardSet.Crown == player2Info.CardSet.Crown) ? Result.Draw : Result.Player2CharacterRuleWin,
                    (Card.Crown, Card.Sheild) or (Card.Sheild, Card.Dagger) or (Card.Dagger, Card.Crown) => Result.Player1Win,
                    (Card.Crown, Card.Dagger) or (Card.Dagger, Card.Sheild) => Result.Player2Win,
                    (Card.Sheild, Card.Crown) => (player1Info.CardSet.Crown == 0 || player2Info.CardSet.Crown == 0) ? Result.Draw : Result.Player2Win,
                    (Card.Sheild, Card.Sheild) => (player1Info.CardSet.Sheild > player2Info.CardSet.Sheild) ? Result.Player1CharacterRuleWin :
                                        (player1Info.CardSet.Sheild == player2Info.CardSet.Sheild) ? Result.Draw : Result.Player2CharacterRuleWin,
                    (Card.Dagger, Card.Dagger) => (player1Info.CardSet.Crown < player2Info.CardSet.Crown) ? Result.Player1CharacterRuleWin :
                                        (player1Info.CardSet.Crown == player2Info.CardSet.Crown) ? Result.Draw : Result.Player2CharacterRuleWin,
                    _ => throw new Exception("Invalid card combination"),
                };
            }

            public static Result SoldierRule(PlayerInfoVM player1Info, PlayerInfoVM player2Info)
            {
                return (player1Info.CurrentChosenCard, player2Info.CurrentChosenCard) switch
                {
                    (Card.Crown, Card.Crown) => (player1Info.CardSet.Sheild > player2Info.CardSet.Sheild) ? Result.Player1CharacterRuleWin :
                                        (player1Info.CardSet.Sheild < player2Info.CardSet.Sheild) ? Result.Player2CharacterRuleWin : Result.Draw,
                    (Card.Crown, Card.Sheild) or (Card.Sheild, Card.Dagger) or (Card.Dagger, Card.Crown) => Result.Player1Win,
                    (Card.Crown, Card.Dagger) or (Card.Sheild, Card.Crown) or (Card.Dagger, Card.Sheild) => Result.Player2Win,
                    (Card.Sheild, Card.Sheild) => (player1Info.CardSet.Sheild > player2Info.CardSet.Sheild) ? Result.Player1CharacterRuleWin :
                                        (player1Info.CardSet.Sheild < player2Info.CardSet.Sheild) ? Result.Player2CharacterRuleWin : Result.Draw,
                    (Card.Dagger, Card.Dagger) => (player1Info.CardSet.Sheild < player2Info.CardSet.Sheild) ? Result.Player1CharacterRuleWin :
                                        (player1Info.CardSet.Sheild > player2Info.CardSet.Sheild) ? Result.Player2CharacterRuleWin : Result.Draw,
                    _ => throw new Exception("Invalid card combination"),
                };
            }
        }
    }

    public class GameServiceException(string message) : Exception(message)
    {
    }
}
