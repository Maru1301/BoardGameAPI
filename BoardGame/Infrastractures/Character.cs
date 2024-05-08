using BoardGame.Models.EFModels;

namespace BoardGame.Infrastractures
{
    public static class CharacterFuncExtensions
    {
        public static (Card, Card) GetLastOpenedCard(RoundCards playerChosenCards, RoundCards player2ChosenCards)
        {
            var lastOpenCard1 = playerChosenCards.LastOpened switch
            {
                1 => playerChosenCards.Card1,
                2 => playerChosenCards.Card2,
                3 => playerChosenCards.Card3,
                _ => throw new NotImplementedException(),
            };

            var lastOpenCard2 = player2ChosenCards.LastOpened switch
            {
                1 => player2ChosenCards.Card1,
                2 => player2ChosenCards.Card2,
                3 => player2ChosenCards.Card3,
                _ => throw new NotImplementedException(),
            };

            return (lastOpenCard1, lastOpenCard2);
        }
    }
    public static class Assassin
    {
        public static CardSet StartHand { get; set; } = new CardSet() 
        {
            Crown = 2,
            Sheild = 2,
            Dagger = 5
        };
        public static Result Rule(PlayerRoundInfo player1Info, PlayerRoundInfo player2Info)
        {
            var (lastOpenCard1, lastOpenCard2) = CharacterFuncExtensions.GetLastOpenedCard(player1Info.ChosenCards, player2Info.ChosenCards);

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

    public static class Deceiver
    {
        public static CardSet StartHand { get; set; } = new CardSet()
        {
            Crown = 4,
            Sheild = 1,
            Dagger = 4
        };
        public static Result Rule(PlayerRoundInfo player1Info, PlayerRoundInfo player2Info)
        {
            var (lastOpenCard1, lastOpenCard2) = CharacterFuncExtensions.GetLastOpenedCard(player1Info.ChosenCards, player2Info.ChosenCards);

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

    public static class Knight 
    {
        public static CardSet StartHand { get; set; } = new CardSet()
        {
            Crown = 3,
            Sheild = 5,
            Dagger = 1
        };
        public static Result Rule(PlayerRoundInfo player1Info, PlayerRoundInfo player2Info)
        {
            var (lastOpenCard1, lastOpenCard2) = CharacterFuncExtensions.GetLastOpenedCard(player1Info.ChosenCards, player2Info.ChosenCards);

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
    public static class Lobbyist 
    {
        public static CardSet StartHand { get; set; } = new CardSet()
        {
            Crown = 5,
            Sheild = 2,
            Dagger = 2
        };
        public static Result Rule(PlayerRoundInfo player1Info, PlayerRoundInfo player2Info)
        {
            var (lastOpenCard1, lastOpenCard2) = CharacterFuncExtensions.GetLastOpenedCard(player1Info.ChosenCards, player2Info.ChosenCards);

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
    public static class Lord 
    {
        public static CardSet StartHand { get; set; } = new CardSet()
        {
            Crown = 0,
            Sheild = 6,
            Dagger = 3
        };
        public static Result Rule(PlayerRoundInfo player1Info, PlayerRoundInfo player2Info)
        {
            var (lastOpenCard1, lastOpenCard2) = CharacterFuncExtensions.GetLastOpenedCard(player1Info.ChosenCards, player2Info.ChosenCards);

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
    public static class Soldier 
    {
        public static CardSet StartHand { get; set; } = new CardSet()
        {
            Crown = 2,
            Sheild = 5,
            Dagger = 2
        };
        public static Result Rule(PlayerRoundInfo player1Info, PlayerRoundInfo player2Info)
        {
            var (lastOpenCard1, lastOpenCard2) = CharacterFuncExtensions.GetLastOpenedCard(player1Info.ChosenCards, player2Info.ChosenCards);

            return (lastOpenCard1, lastOpenCard2) switch
            {
                (Card.Crown, Card.Crown) => (player1Info.Hand.Sheild > player2Info.Hand.Sheild) ? Result.Player1CharacterRuleWin :
                                    (player1Info.Hand.Sheild < player2Info.Hand.Sheild) ? Result.Player2CharacterRuleWin : Result.Draw,
                (Card.Crown, Card.Sheild) or (Card.Sheild, Card.Dagger) or (Card.Dagger, Card.Crown) => Result.Player1Win,
                (Card.Crown, Card.Dagger) or (Card.Sheild, Card.Crown) or (Card.Dagger, Card.Sheild) => Result.Player2Win,
                (Card.Sheild, Card.Sheild) => (player1Info.Hand.Sheild > player2Info.Hand.Sheild) ? Result.Player1CharacterRuleWin :
                                    (player1Info.Hand.Sheild < player2Info.Hand.Sheild) ? Result.Player2CharacterRuleWin : Result.Draw,
                (Card.Dagger, Card.Dagger) => (player1Info.Hand.Sheild < player2Info.Hand.Sheild) ? Result.Player1CharacterRuleWin :
                                    (player1Info.Hand   .Sheild > player2Info.Hand.Sheild) ? Result.Player2CharacterRuleWin : Result.Draw,
                _ => throw new Exception("Invalid card combination"),
            };
        }
    }
}
