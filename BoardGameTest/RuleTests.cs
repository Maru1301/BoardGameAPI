using BoardGame.Infrastractures;
using BoardGame.Models.EFModels;
using BoardGame.Services;
using BoardGame.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BoardGameTest
{
    public class RuleTests
    {
        private static readonly IServiceProvider ServiceProvider;

        static RuleTests()
        {
            var services = new ServiceCollection();

            // Register the AssassinRule service
            services.AddTransient<IGameService, GameService>();

            // Register mocks for dependencies (if any)
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<AppDbContext>(options =>
                options.UseMongoDB("mongodb+srv://Maru:13011821@cluster0.r3hywvh.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0", "BoardGameDB"), ServiceLifetime.Scoped);

            ServiceProvider = services.BuildServiceProvider();
        }

        public class AssassinRuleTests()
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
                // Get the service instance from the static field
                var service = ServiceProvider.GetService<IGameService>();

                // Act
                var result = service!.MapRule(player1.Character)(player1, player2);

                // Assert
                Assert.Equal(result, expected);
            }

            [Fact]
            public void TestAssassinRule_InvalidCardCombination()
            {
                // Get the service instance from the static field
                var service = ServiceProvider.GetService<IGameService>();

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
                var exception = Assert.Throws<Exception>(() => service!.MapRule(player1.Character)(player1, player2));
                Assert.Equal("Invalid card combination", exception.Message);
            }
        }

        public class DeceiverRuleTests()
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
                // Get the service instance from the static field
                var service = ServiceProvider.GetService<IGameService>();

                // Act
                var result = service!.MapRule(player1.Character)(player1, player2);

                // Assert
                Assert.Equal(result, expected);
            }

            [Fact]
            public void TestInvalidCardCombination()
            {
                // Get the service instance from the static field
                var service = ServiceProvider.GetService<IGameService>();

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
                var exception = Assert.Throws<Exception>(() => service!.MapRule(player1.Character)(player1, player2));
                Assert.Equal("Invalid card combination", exception.Message);
            }
        }
    }
}
