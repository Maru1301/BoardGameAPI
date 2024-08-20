using BoardGame.Models.EFModels;

namespace BoardGame.Infrastractures
{
    public abstract class CharacterBase
    {
        public abstract CardSet StartHand { get; set; }

        public Result Rule(PlayerRoundInfo player1Info, PlayerRoundInfo player2Info)
        {
            if(player1Info.LastOpened > 2 || player2Info.LastOpened > 2)
            {
                throw new ArgumentOutOfRangeException("LastOpened", "lastopened out of range");
            }

            var lastOpenCard1 = player1Info.ChosenCards[player1Info.LastOpened];
            var lastOpenCard2 = player2Info.ChosenCards[player2Info.LastOpened];

            var Cards = new List<Card>() { Card.Crown, Card.Sheild, Card.Dagger };

            if (!Cards.Contains(lastOpenCard1) || !Cards.Contains(lastOpenCard2)) 
            {
                throw new Exception("Invalid card combination");   
            }

            return DetermineResult(lastOpenCard1, lastOpenCard2, player1Info, player2Info);
        }

        protected abstract Result DetermineResult(Card lastOpenCard1, Card lastOpenCard2, PlayerRoundInfo player1Info, PlayerRoundInfo player2Info);
    }

    public class Assassin : CharacterBase
    {
        public override CardSet StartHand { get; set; } = new CardSet()
        {
            Crown = 2,
            Sheild = 2,
            Dagger = 5
        };
        protected override Result DetermineResult(Card lastOpenCard1, Card lastOpenCard2, PlayerRoundInfo player1Info, PlayerRoundInfo player2Info)
        {
            return (lastOpenCard1, lastOpenCard2) switch
            {
                (Card.Crown, Card.Crown) or (Card.Sheild, Card.Sheild) => Result.Draw,
                (Card.Crown, Card.Sheild) or (Card.Sheild, Card.Dagger) or (Card.Dagger, Card.Crown) => Result.Player1Win,
                (Card.Crown, Card.Dagger) or (Card.Sheild, Card.Crown) or (Card.Dagger, Card.Sheild) => Result.Player2Win,
                (Card.Dagger, Card.Dagger) => player1Info.Hand.Dagger > player2Info.Hand.Dagger && player1Info.Hand.Dagger > 2 ? Result.Player1CharacterRuleWin :
                                        player1Info.Hand.Dagger < player2Info.Hand.Dagger && player2Info.Hand.Dagger > 2 ? Result.Player2CharacterRuleWin : Result.Draw,
                _ => throw new Exception("Invalid card combination"),
            };
        }
    }

    public class Deceiver : CharacterBase
    {
        public override CardSet StartHand { get; set; } = new CardSet()
        {
            Crown = 4,
            Sheild = 1,
            Dagger = 4
        };
        protected override Result DetermineResult(Card lastOpenCard1, Card lastOpenCard2, PlayerRoundInfo player1Info, PlayerRoundInfo player2Info)
        {
            return (lastOpenCard1, lastOpenCard2) switch
            {
                (Card.Crown, Card.Crown) => player1Info.Hand.Sheild > player2Info.Hand.Sheild ? Result.Player1CharacterRuleWin :
                                        player1Info.Hand.Sheild < player2Info.Hand.Sheild ? Result.Player2CharacterRuleWin : Result.Draw,
                (Card.Crown, Card.Sheild) or (Card.Sheild, Card.Dagger) or (Card.Dagger, Card.Crown) => Result.Player1Win,
                (Card.Crown, Card.Dagger) or (Card.Sheild, Card.Crown) or (Card.Dagger, Card.Sheild) => Result.Player2Win,
                (Card.Sheild, Card.Sheild) or (Card.Dagger, Card.Dagger) => player1Info.Hand.Sheild < player2Info.Hand.Sheild ? Result.Player1CharacterRuleWin :
                                        player1Info.Hand.Sheild > player2Info.Hand.Sheild ? Result.Player2CharacterRuleWin : Result.Draw,
                _ => throw new Exception("Invalid card combination"),
            };
        }
    }

    public class Knight : CharacterBase
    {
        public override CardSet StartHand { get; set; } = new CardSet()
        {
            Crown = 3,
            Sheild = 5,
            Dagger = 1
        };
        protected override Result DetermineResult(Card lastOpenCard1, Card lastOpenCard2, PlayerRoundInfo player1Info, PlayerRoundInfo player2Info)
        {
            return (lastOpenCard1, lastOpenCard2) switch
            {
                (Card.Crown, Card.Crown) => player1Info.Hand.Dagger < player2Info.Hand.Dagger ? Result.Player1CharacterRuleWin :
                                        player1Info.Hand.Dagger > player2Info.Hand.Dagger ? Result.Player2CharacterRuleWin : Result.Draw,
                (Card.Crown, Card.Sheild) or (Card.Sheild, Card.Dagger) => Result.Player1Win,
                (Card.Crown, Card.Dagger) => player1Info.Hand.Dagger > player2Info.Hand.Dagger ? Result.Draw : Result.Player2Win,
                (Card.Sheild, Card.Crown) or (Card.Dagger, Card.Sheild) => Result.Player2Win,
                (Card.Sheild, Card.Sheild) => player1Info.Hand.Dagger < player2Info.Hand.Dagger ? Result.Player1CharacterRuleWin :
                                        player1Info.Hand.Dagger == player2Info.Hand.Dagger ? Result.Draw : Result.Player2CharacterRuleWin,
                (Card.Dagger, Card.Crown) => player1Info.Hand.Dagger < player2Info.Hand.Dagger ? Result.Draw : Result.Player1Win,
                (Card.Dagger, Card.Dagger) => player1Info.Hand.Dagger > player2Info.Hand.Dagger ? Result.Player1CharacterRuleWin :
                                        player1Info.Hand.Dagger < player2Info.Hand.Dagger ? Result.Player2CharacterRuleWin : Result.Draw,
                _ => throw new Exception("Invalid card combination"),
            };
        }
    }
    public class Lobbyist : CharacterBase
    {
        public override CardSet StartHand { get; set; } = new CardSet()
        {
            Crown = 5,
            Sheild = 2,
            Dagger = 2
        };
        protected override Result DetermineResult(Card lastOpenCard1, Card lastOpenCard2, PlayerRoundInfo player1Info, PlayerRoundInfo player2Info)
        {
            return (lastOpenCard1, lastOpenCard2) switch
            {
                (Card.Crown, Card.Crown) => (player1Info.Hand.Crown > 2 && player1Info.Hand.Crown > player2Info.Hand.Crown) ? Result.Player1CharacterRuleWin :
                                        (player2Info.Hand.Crown > 2 && player2Info.Hand.Crown > player1Info.Hand.Crown) ? Result.Player2CharacterRuleWin : Result.Draw,
                (Card.Crown, Card.Sheild) or (Card.Sheild, Card.Dagger) or (Card.Dagger, Card.Crown) => Result.Player1Win,
                (Card.Crown, Card.Dagger) or (Card.Sheild, Card.Crown) or (Card.Dagger, Card.Sheild) => Result.Player2Win,
                (Card.Sheild, Card.Sheild) => (player1Info.Hand.Crown > player2Info.Hand.Crown) ? Result.Player1CharacterRuleWin :
                                    (player1Info.Hand.Crown == player2Info.Hand.Crown) ? Result.Draw : Result.Player2CharacterRuleWin,
                (Card.Dagger, Card.Dagger) => Result.Draw,
                _ => throw new Exception("Invalid card combination"),
            };
        }
    }
    public class Lord : CharacterBase
    {
        public override CardSet StartHand { get; set; } = new CardSet()
        {
            Crown = 0,
            Sheild = 6,
            Dagger = 3
        };
        protected override Result DetermineResult(Card lastOpenCard1, Card lastOpenCard2, PlayerRoundInfo player1Info, PlayerRoundInfo player2Info)
        {
            return (lastOpenCard1, lastOpenCard2) switch
            {
                (Card.Crown, Card.Crown) => (player1Info.Hand.Crown > player2Info.Hand.Crown) ? Result.Player1CharacterRuleWin :
                                    (player1Info.Hand.Crown == player2Info.Hand.Crown) ? Result.Draw : Result.Player2CharacterRuleWin,
                (Card.Crown, Card.Sheild) or (Card.Sheild, Card.Dagger) or (Card.Dagger, Card.Crown) => Result.Player1Win,
                (Card.Crown, Card.Dagger) or (Card.Dagger, Card.Sheild) => Result.Player2Win,
                (Card.Sheild, Card.Crown) => (player1Info.Hand.Crown == 0 || player2Info.Hand.Crown == 0) ? Result.Draw : Result.Player2Win,
                (Card.Sheild, Card.Sheild) => (player1Info.Hand.Sheild > player2Info.Hand.Sheild) ? Result.Player1CharacterRuleWin :
                                    (player1Info.Hand.Sheild == player2Info.Hand.Sheild) ? Result.Draw : Result.Player2CharacterRuleWin,
                (Card.Dagger, Card.Dagger) => (player1Info.Hand.Crown < player2Info.Hand.Crown) ? Result.Player1CharacterRuleWin :
                                    (player1Info.Hand.Crown == player2Info.Hand.Crown) ? Result.Draw : Result.Player2CharacterRuleWin,
                _ => throw new Exception("Invalid card combination"),
            };
        }
    }
    public class Soldier : CharacterBase
    {
        public override CardSet StartHand { get; set; } = new CardSet()
        {
            Crown = 2,
            Sheild = 5,
            Dagger = 2
        };
        protected override Result DetermineResult(Card lastOpenCard1, Card lastOpenCard2, PlayerRoundInfo player1Info, PlayerRoundInfo player2Info)
        {
            return (lastOpenCard1, lastOpenCard2) switch
            {
                (Card.Crown, Card.Crown) => (player1Info.Hand.Sheild > player2Info.Hand.Sheild) ? Result.Player1CharacterRuleWin :
                                    (player1Info.Hand.Sheild < player2Info.Hand.Sheild) ? Result.Player2CharacterRuleWin : Result.Draw,
                (Card.Crown, Card.Sheild) or (Card.Sheild, Card.Dagger) or (Card.Dagger, Card.Crown) => Result.Player1Win,
                (Card.Crown, Card.Dagger) or (Card.Sheild, Card.Crown) or (Card.Dagger, Card.Sheild) => Result.Player2Win,
                (Card.Sheild, Card.Sheild) => (player1Info.Hand.Sheild > player2Info.Hand.Sheild) ? Result.Player1CharacterRuleWin :
                                    (player1Info.Hand.Sheild < player2Info.Hand.Sheild) ? Result.Player2CharacterRuleWin : Result.Draw,
                (Card.Dagger, Card.Dagger) => (player1Info.Hand.Sheild < player2Info.Hand.Sheild) ? Result.Player1CharacterRuleWin :
                                    (player1Info.Hand.Sheild > player2Info.Hand.Sheild) ? Result.Player2CharacterRuleWin : Result.Draw,
                _ => throw new Exception("Invalid card combination"),
            };
        }
    }
}