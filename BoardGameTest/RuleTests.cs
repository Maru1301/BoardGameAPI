using BoardGame.Infrastractures;
using BoardGame.Models.EFModels;
using BoardGame.Services;

namespace BoardGameTest
{
    public class RuleTests
    {
        public class AssassinRuleTests
        {
            public static IEnumerable<object[]> RuleData()
            {
                yield return new object[]
                {
                    new PlayerRoundInfo
                    {
                        Character = Character.Assassin,
                        Hand = new(),
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Crown,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    new PlayerRoundInfo
                    {
                        Character = Character.Assassin,
                        Hand = new(),
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Crown,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    Result.Draw
                };
                yield return new object[]
                {
                    new PlayerRoundInfo
                    {
                        Character = Character.Assassin,
                        Hand = new(),
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Sheild,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    new PlayerRoundInfo
                    {
                        Character = Character.Assassin,
                        Hand = new(),
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Sheild,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    Result.Draw
                };
                yield return new object[]
                {
                    new PlayerRoundInfo
                    {
                        Character = Character.Assassin,
                        Hand = new(),
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Crown,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    new PlayerRoundInfo
                    {
                        Character = Character.Assassin,
                        Hand = new(),
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Sheild,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    Result.Player1Win
                };
                yield return new object[]
                {
                    new PlayerRoundInfo
                    {
                        Character = Character.Assassin,
                        Hand = new(),
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Sheild,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    new PlayerRoundInfo
                    {
                        Character = Character.Assassin,
                        Hand = new(),
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Dagger,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    Result.Player1Win
                };
                yield return new object[]
                {
                    new PlayerRoundInfo
                    {
                        Character = Character.Assassin,
                        Hand = new(),
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Dagger,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    new PlayerRoundInfo
                    {
                        Character = Character.Assassin,
                        Hand = new(),
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Crown,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    Result.Player1Win
                };
                yield return new object[]
                {
                    new PlayerRoundInfo
                    {
                        Character = Character.Assassin,
                        Hand = new(),
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Crown,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    new PlayerRoundInfo
                    {
                        Character = Character.Assassin,
                        Hand = new(),
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Dagger,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    Result.Player2Win
                };
                yield return new object[]
                {
                    new PlayerRoundInfo
                    {
                        Character = Character.Assassin,
                        Hand = new(),
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Dagger,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    new PlayerRoundInfo
                    {
                        Character = Character.Assassin,
                        Hand = new(),
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Sheild,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    Result.Player2Win
                };
                yield return new object[]
                {
                    new PlayerRoundInfo
                    {
                        Character = Character.Assassin,
                        Hand = new(),
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Sheild,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    new PlayerRoundInfo
                    {
                        Character = Character.Assassin,
                        Hand = new(),
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Crown,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    Result.Player2Win
                };
                yield return new object[]
                {
                    new PlayerRoundInfo
                    {
                        Character = Character.Assassin,
                        Hand = new() { Dagger = 3},
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Dagger,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    new PlayerRoundInfo
                    {
                        Character = Character.Assassin,
                        Hand = new() { Dagger = 2 },
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Dagger,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    Result.Player1CharacterRuleWin
                };
                yield return new object[]
                {
                    new PlayerRoundInfo
                    {
                        Character = Character.Assassin,
                        Hand = new() { Dagger = 2},
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Dagger,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    new PlayerRoundInfo
                    {
                        Character = Character.Assassin,
                        Hand = new() { Dagger = 3 },
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Dagger,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    Result.Player2CharacterRuleWin
                };
                yield return new object[]
                {
                    new PlayerRoundInfo
                    {
                        Character = Character.Assassin,
                        Hand = new() { Dagger = 2},
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Dagger,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    new PlayerRoundInfo
                    {
                        Character = Character.Assassin,
                        Hand = new() { Dagger = 2 },
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Dagger,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    Result.Draw
                };
                yield return new object[]
                {
                    new PlayerRoundInfo
                    {
                        Character = Character.Assassin,
                        Hand = new() { Dagger = 2},
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Dagger,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    new PlayerRoundInfo
                    {
                        Character = Character.Assassin,
                        Hand = new() { Dagger = 1 },
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Dagger,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    Result.Draw
                };
                yield return new object[]
                {
                    new PlayerRoundInfo
                    {
                        Character = Character.Assassin,
                        Hand = new() { Dagger = 1},
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Dagger,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    new PlayerRoundInfo
                    {
                        Character = Character.Assassin,
                        Hand = new() { Dagger = 2 },
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Dagger,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    Result.Draw
                };
            }

            [Theory]
            [MemberData(nameof(RuleData))]
            public void TestAssassinRule_NormalRule(PlayerRoundInfo player1, PlayerRoundInfo player2, Result expected)
            {
                // Act
                var result = GameService.MapRule(player1.Character)(player1, player2);

                // Assert
                Assert.Equal(result, expected);
            }

            [Fact]
            public void TestAssassinRule_InvalidCardCombination()
            {
                // Arrange
                var player1 = new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new(),
                    ChosenCards = new RoundCards
                    {
                        Card1 = (Card)100,
                        Card2 = Card.Crown,
                        Card3 = Card.Crown,
                        LastOpened = 1,
                    }
                };
                var player2 = new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new(),
                    ChosenCards = new RoundCards
                    {
                        Card1 = (Card)200,
                        Card2 = Card.Crown,
                        Card3 = Card.Crown,
                        LastOpened = 1,
                    }
                };

                // Act and Assert
                var exception = Assert.Throws<Exception>(() => GameService.MapRule(player1.Character)(player1, player2));
                Assert.Equal("Invalid card combination", exception.Message);
            }
        }

        public class DeceiverRuleTests
        {
            public static IEnumerable<object[]> RuleData()
            {
                yield return new object[]
                {
                    new PlayerRoundInfo
                    {
                        Character = Character.Deceiver,
                        Hand = new() { Sheild = 2 },
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Crown,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    new PlayerRoundInfo
                    {
                        Character = Character.Deceiver,
                        Hand = new() { Sheild = 1},
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Crown,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    Result.Player1CharacterRuleWin
                };
                yield return new object[]
                {
                    new PlayerRoundInfo
                    {
                        Character = Character.Deceiver,
                        Hand = new() { Sheild = 1 },
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Crown,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    new PlayerRoundInfo
                    {
                        Character = Character.Deceiver,
                        Hand = new() { Sheild = 2},
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Crown,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    Result.Player2CharacterRuleWin
                };
                yield return new object[]
                {
                    new PlayerRoundInfo
                    {
                        Character = Character.Deceiver,
                        Hand = new() { Sheild = 2 },
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Crown,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    new PlayerRoundInfo
                    {
                        Character = Character.Deceiver,
                        Hand = new() { Sheild = 2},
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Crown,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    Result.Draw
                };
                yield return new object[]
                {
                    new PlayerRoundInfo
                    {
                        Character = Character.Deceiver,
                        Hand = new(),
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Sheild,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    new PlayerRoundInfo
                    {
                        Character = Character.Deceiver,
                        Hand = new(),
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Sheild,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    Result.Draw
                };
                yield return new object[]
                {
                    new PlayerRoundInfo
                    {
                        Character = Character.Deceiver,
                        Hand = new(),
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Crown,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    new PlayerRoundInfo
                    {
                        Character = Character.Deceiver,
                        Hand = new(),
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Sheild,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    Result.Player1Win
                };
                yield return new object[]
                {
                    new PlayerRoundInfo
                    {
                        Character = Character.Deceiver,
                        Hand = new(),
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Sheild,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    new PlayerRoundInfo
                    {
                        Character = Character.Deceiver,
                        Hand = new(),
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Dagger,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    Result.Player1Win
                };
                yield return new object[]
                {
                    new PlayerRoundInfo
                    {
                        Character = Character.Deceiver,
                        Hand = new(),
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Dagger,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    new PlayerRoundInfo
                    {
                        Character = Character.Deceiver,
                        Hand = new(),
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Crown,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    Result.Player1Win
                };
                yield return new object[]
                {
                    new PlayerRoundInfo
                    {
                        Character = Character.Deceiver,
                        Hand = new(),
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Crown,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    new PlayerRoundInfo
                    {
                        Character = Character.Deceiver,
                        Hand = new(),
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Dagger,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    Result.Player2Win
                };
                yield return new object[]
                {
                    new PlayerRoundInfo
                    {
                        Character = Character.Deceiver,
                        Hand = new(),
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Sheild,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    new PlayerRoundInfo
                    {
                        Character = Character.Deceiver,
                        Hand = new(),
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Crown,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    Result.Player2Win
                };
                yield return new object[]
                {
                    new PlayerRoundInfo
                    {
                        Character = Character.Deceiver,
                        Hand = new(),
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Dagger,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    new PlayerRoundInfo
                    {
                        Character = Character.Deceiver,
                        Hand = new(),
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Sheild,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    Result.Player2Win
                };
                yield return new object[]
                {
                    new PlayerRoundInfo
                    {
                        Character = Character.Deceiver,
                        Hand = new() { Sheild = 1 },
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Sheild,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    new PlayerRoundInfo
                    {
                        Character = Character.Deceiver,
                        Hand = new() { Sheild = 2 },
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Sheild,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    Result.Player1CharacterRuleWin
                };
                yield return new object[]
                {
                    new PlayerRoundInfo
                    {
                        Character = Character.Deceiver,
                        Hand = new() { Sheild = 2 },
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Sheild,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    new PlayerRoundInfo
                    {
                        Character = Character.Deceiver,
                        Hand = new() { Sheild = 1 },
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Sheild,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    Result.Player2CharacterRuleWin
                };
                yield return new object[]
                {
                    new PlayerRoundInfo
                    {
                        Character = Character.Deceiver,
                        Hand = new() { Sheild = 2 },
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Sheild,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    new PlayerRoundInfo
                    {
                        Character = Character.Deceiver,
                        Hand = new() { Sheild = 2 },
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Sheild,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    Result.Draw
                };
                yield return new object[]
                {
                    new PlayerRoundInfo
                    {
                        Character = Character.Deceiver,
                        Hand = new() { Sheild = 1 },
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Dagger,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    new PlayerRoundInfo
                    {
                        Character = Character.Deceiver,
                        Hand = new() { Sheild = 2 },
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Dagger,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    Result.Player1CharacterRuleWin
                };
                yield return new object[]
                {
                    new PlayerRoundInfo
                    {
                        Character = Character.Deceiver,
                        Hand = new() { Sheild = 2 },
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Dagger,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    new PlayerRoundInfo
                    {
                        Character = Character.Deceiver,
                        Hand = new() { Sheild = 1 },
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Dagger,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    Result.Player2CharacterRuleWin
                };
                yield return new object[]
                {
                    new PlayerRoundInfo
                    {
                        Character = Character.Deceiver,
                        Hand = new() { Sheild = 2 },
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Dagger,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    new PlayerRoundInfo
                    {
                        Character = Character.Deceiver,
                        Hand = new() { Sheild = 2 },
                        ChosenCards = new RoundCards
                        {
                            Card1 = Card.Dagger,
                            Card2 = Card.Crown,
                            Card3 = Card.Crown,
                            LastOpened = 1,
                        }
                    },
                    Result.Draw
                };
            }

            [Theory]
            [MemberData(nameof(RuleData))]
            public void TesRule(PlayerRoundInfo player1, PlayerRoundInfo player2, Result expected)
            {
                // Act
                var result = GameService.MapRule(player1.Character)(player1, player2);

                // Assert
                Assert.Equal(result, expected);
            }

            [Fact]
            public void TestInvalidCardCombination()
            {
                // Arrange
                var player1 = new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new(),
                    ChosenCards = new RoundCards
                    {
                        Card1 = (Card)100,
                        Card2 = Card.Crown,
                        Card3 = Card.Crown,
                        LastOpened = 1,
                    }
                };
                var player2 = new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new(),
                    ChosenCards = new RoundCards
                    {
                        Card1 = (Card)200,
                        Card2 = Card.Crown,
                        Card3 = Card.Crown,
                        LastOpened = 1,
                    }
                };

                // Act and Assert
                var exception = Assert.Throws<Exception>(() => GameService.MapRule(player1.Character)(player1, player2));
                Assert.Equal("Invalid card combination", exception.Message);
            }
        }
    }
}
