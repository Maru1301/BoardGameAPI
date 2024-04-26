using BoardGame.Infrastractures;
using static BoardGame.Models.ViewModels.GameVMs;
using static BoardGame.Services.GameService;

namespace BoardGrmeTest
{
    public class TestAssassinRule
    {
        private static readonly object[] NormalRuleData =
        [
            new object[] {
                new List<CardSet>() { new() , new() },
                new List<Card>() { Card.Crown, Card.Crown },
                Result.Draw
            },
            new object[] {
                new List<CardSet>() { new() , new() },
                new List<Card>() { Card.Sheild, Card.Sheild },
                Result.Draw
            },
            new object[]
            {
                new List<CardSet>() { new() , new() },
                new List<Card>() { Card.Crown, Card.Sheild },
                Result.BasicWin
            },
            new object[]
            {
                new List<CardSet>() { new() , new() },
                new List<Card>() { Card.Sheild, Card.Dagger },
                Result.BasicWin
            },
            new object[]
            {
                new List<CardSet>() { new() , new() },
                new List<Card>() { Card.Dagger, Card.Crown },
                Result.BasicWin
            },
            new object[]
            {
                new List<CardSet>() { new() , new() },
                new List<Card>() { Card.Crown, Card.Dagger },
                Result.BasicLose
            },
            new object[]
            {
                new List<CardSet>() { new() , new() },
                new List<Card>() { Card.Dagger, Card.Sheild },
                Result.BasicLose
            },
            new object[]
            {
                new List<CardSet>() { new() , new() },
                new List<Card>() { Card.Sheild, Card.Crown },
                Result.BasicLose
            },
        ];

        private static readonly object[] CharacterRuleData =
        [
            new object[]
            {
                new List<CardSet>() { new() { Dagger = 3} , new() { Dagger = 2 } },
                new List<Card>() { Card.Dagger, Card.Dagger },
                Result.CharacterRuleWin
            },
            new object[]
            {
                new List<CardSet>() { new() { Dagger = 2} , new() { Dagger = 3 } },
                new List<Card>() { Card.Dagger, Card.Dagger },
                Result.CharacterRuleLose
            },
            new object[]
            {
                new List<CardSet>() { new() { Dagger = 2} , new() { Dagger = 2 } },
                new List<Card>() { Card.Dagger, Card.Dagger },
                Result.Draw
            },
            new object[]
            {
                new List<CardSet>() { new() { Dagger = 2} , new() { Dagger = 1 } },
                new List<Card>() { Card.Dagger, Card.Dagger },
                Result.Draw
            },
            new object[]
            {
                new List<CardSet>() { new() { Dagger = 1} , new() { Dagger = 2 } },
                new List<Card>() { Card.Dagger, Card.Dagger },
                Result.Draw
            },
        ];


        [Test]
        [TestCaseSource(nameof(NormalRuleData))]
        public void TestAssassinRule_NormalRule(List<CardSet> cardSets, List<Card> currentChosen, Result expected)
        {
            // Arrange
            var player1Info = new PlayerInfoVM(cardSets[0], currentChosen[0]);
            var player2Info = new PlayerInfoVM(cardSets[1], currentChosen[1]);

            // Act
            var result = Rule.AssassinRule(player1Info, player2Info);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCaseSource(nameof(CharacterRuleData))]
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
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TestAssassinRule_InvalidCardCombination()
        {
            // Arrange
            var player1Info = new PlayerInfoVM(new CardSet(), (Card)100 );
            var player2Info = new PlayerInfoVM(new CardSet(), (Card)200);

            // Act and Assert
            Assert.Throws<Exception>(() => Rule.AssassinRule(player1Info, player2Info), "Invalid card combination");
        }
    }
}