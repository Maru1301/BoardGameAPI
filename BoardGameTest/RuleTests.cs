using BoardGame.Infrastractures;
using static BoardGame.Models.ViewModels.GameVMs;
using static BoardGame.Services.GameService;

namespace BoardGameTest
{
    public class RuleTests
    {
        public class AssassinRuleTests
        {
            public static IEnumerable<object[]> NormalRuleData()
            {
                yield return new object[]
                {
                    new List<CardSet>() { new() , new() },
                    new List<Card>() { Card.Crown, Card.Crown },
                    Result.Draw
                };
                yield return new object[]
                {
                    new List<CardSet>() { new(), new() },
                    new List<Card>() { Card.Sheild, Card.Sheild },
                    Result.Draw
                };
                yield return new object[]
                {
                    new List<CardSet>() { new() , new() },
                    new List<Card>() { Card.Crown, Card.Sheild },
                    Result.Player1Win
                };
                yield return new object[]
                {
                    new List<CardSet>() { new() , new() },
                    new List<Card>() { Card.Sheild, Card.Dagger },
                    Result.Player1Win
                };
                yield return new object[]
                {
                    new List<CardSet>() { new() , new() },
                    new List<Card>() { Card.Dagger, Card.Crown },
                    Result.Player1Win
                };
                yield return new object[]
                {
                    new List<CardSet>() { new() , new() },
                    new List<Card>() { Card.Crown, Card.Dagger },
                    Result.Player2Win
                };
                yield return new object[]
                {
                    new List<CardSet>() { new() , new() },
                    new List<Card>() { Card.Dagger, Card.Sheild },
                    Result.Player2Win
                };
                yield return new object[]
                {
                    new List<CardSet>() { new() , new() },
                    new List<Card>() { Card.Sheild, Card.Crown },
                    Result.Player2Win
                };
            }

            public static IEnumerable<object[]> CharacterRuleData()
            {
                yield return new object[]
                {
                    new List<CardSet>() { new() { Dagger = 3} , new() { Dagger = 2 } },
                    new List<Card>() { Card.Dagger, Card.Dagger },
                    Result.Player1CharacterRuleWin
                };
                yield return new object[]
                {
                    new List<CardSet>() { new() { Dagger = 2} , new() { Dagger = 3 } },
                    new List<Card>() { Card.Dagger, Card.Dagger },
                    Result.Player2CharacterRuleWin
                };
                yield return new object[]
                {
                    new List<CardSet>() { new() { Dagger = 2} , new() { Dagger = 2 } },
                    new List<Card>() { Card.Dagger, Card.Dagger },
                    Result.Draw
                };
                yield return new object[]
                {
                    new List<CardSet>() { new() { Dagger = 2} , new() { Dagger = 1 } },
                    new List<Card>() { Card.Dagger, Card.Dagger },
                    Result.Draw
                };
                yield return new object[]
                {
                    new List<CardSet>() { new() { Dagger = 1} , new() { Dagger = 2 } },
                    new List<Card>() { Card.Dagger, Card.Dagger },
                    Result.Draw
                };
            }


            [Theory]
            [MemberData(nameof(NormalRuleData))]
            public void TestAssassinRule_NormalRule(List<CardSet> cardSets, List<Card> currentChosen, Result expected)
            {
                // Arrange
                var player1Info = new PlayerInfoVM(cardSets[0], currentChosen[0]);
                var player2Info = new PlayerInfoVM(cardSets[1], currentChosen[1]);

                // Act
                var result = Rule.AssassinRule(player1Info, player2Info);

                // Assert
                Assert.Equal(result, expected);
            }

            [Theory]
            [MemberData(nameof(CharacterRuleData))]
            public void TestAssassinRule_CharacterRule(List<CardSet> cardSets, List<Card> currentChosen, Result expected)
            {
                // Arrange
                var player1Info = new PlayerInfoVM(cardSets[0], currentChosen[0]);
                var player2Info = new PlayerInfoVM(cardSets[1], currentChosen[1]);
                Console.WriteLine(player1Info.CardSet.Dagger);
                Console.WriteLine(player2Info.CardSet.Dagger);
                // Act
                var result = Rule.AssassinRule(player1Info, player2Info);

                // Assert
                Assert.Equal(result, expected);
            }

            [Fact]
            public void TestAssassinRule_InvalidCardCombination()
            {
                // Arrange
                var player1Info = new PlayerInfoVM(new CardSet(), (Card)100);
                var player2Info = new PlayerInfoVM(new CardSet(), (Card)200);

                // Act and Assert
                var exception = Assert.Throws<Exception>(() => Rule.AssassinRule(player1Info, player2Info));
                Assert.Equal("Invalid card combination", exception.Message);
            }
        }
    }
}
