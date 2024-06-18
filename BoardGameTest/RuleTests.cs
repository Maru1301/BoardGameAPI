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
        private static readonly RoundCards _CCC = new()
        {
            Card1 = Card.Crown,
            Card2 = Card.Crown,
            Card3 = Card.Crown,
            LastOpened = 1,
        };

        private static readonly RoundCards _SCC = new()
        {
            Card1 = Card.Sheild,
            Card2 = Card.Crown,
            Card3 = Card.Crown,
            LastOpened = 1,
        };

        private static readonly RoundCards _DCC = new()
        {
            Card1 = Card.Dagger,
            Card2 = Card.Crown,
            Card3 = Card.Crown,
            LastOpened = 1,
        };

        public static TheoryData<PlayerRoundInfo, PlayerRoundInfo, Result> AssassinRuleData => new()
        {
            {
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new(),
                    ChosenCards = _CCC
                },
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new(),
                    ChosenCards = _CCC
                },
                Result.Draw
            },
            {
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new(),
                    ChosenCards = _SCC
                },
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new(),
                    ChosenCards = _SCC
                },
                Result.Draw
            },
            {
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new(),
                    ChosenCards = _CCC
                },
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new(),
                    ChosenCards = _SCC
                },
                Result.Player1Win
            },
            {
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new(),
                    ChosenCards = _SCC
                },
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new(),
                    ChosenCards = _DCC
                },
                Result.Player1Win
            },
            {
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new(),
                    ChosenCards = _DCC
                },
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new(),
                    ChosenCards = _CCC
                },
                Result.Player1Win
            },
            {
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new(),
                    ChosenCards = _DCC
                },
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new(),
                    ChosenCards = _DCC
                },
                Result.Draw
            },
            {
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new(),
                    ChosenCards = _CCC
                },
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new(),
                    ChosenCards = _DCC
                },
                Result.Player2Win
            },
            {
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new(),
                    ChosenCards = _DCC
                },
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new(),
                    ChosenCards = _SCC
                },
                Result.Player2Win
            },
            {
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new(),
                    ChosenCards = _DCC
                },
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new(),
                    ChosenCards = _SCC
                },
                Result.Player2Win
            },
            {
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new(),
                    ChosenCards = _SCC
                },
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new(),
                    ChosenCards = _CCC
                },
                Result.Player2Win
            },
            {
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new(),
                    ChosenCards = _SCC
                },
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new(),
                    ChosenCards = _CCC
                },
                Result.Player2Win
            },
            {
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new(),
                    ChosenCards = _SCC
                },
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new(),
                    ChosenCards = _CCC
                },
                Result.Player2Win
            },
            {
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new() { Dagger = 3},
                    ChosenCards = _DCC
                },
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new() { Dagger = 2 },
                    ChosenCards = _DCC
                },
                Result.Player1CharacterRuleWin
            },
            {
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new() { Dagger = 2},
                    ChosenCards = _DCC
                },
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new() { Dagger = 3 },
                    ChosenCards = _DCC
                },
                Result.Player2CharacterRuleWin
            },
                {
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new() { Dagger = 2},
                    ChosenCards = _DCC
                },
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new() { Dagger = 2 },
                    ChosenCards = _DCC
                },
                Result.Draw
            },
            {
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new() { Dagger = 2},
                    ChosenCards = _DCC
                },
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new() { Dagger = 2 },
                    ChosenCards = _DCC
                },
                Result.Draw
            },
            {
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new() { Dagger = 2},
                    ChosenCards = _DCC
                },
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new() { Dagger = 1 },
                    ChosenCards = _DCC
                },
                Result.Draw
            },
            {
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new() { Dagger = 1},
                    ChosenCards = _DCC
                },
                new PlayerRoundInfo
                {
                    Character = Character.Assassin,
                    Hand = new() { Dagger = 2 },
                    ChosenCards = _DCC
                },
                Result.Draw
            }
        };

        public static TheoryData<PlayerRoundInfo, PlayerRoundInfo, Result> DeceiverRuleData => new()
        {
            {
                new PlayerRoundInfo
                {
                    Character = Character.Deceiver,
                    Hand = new() { Sheild = 2 },
                    ChosenCards = _CCC
                },
                new PlayerRoundInfo
                {
                    Character = Character.Deceiver,
                    Hand = new() { Sheild = 1},
                    ChosenCards = _CCC
                },
                Result.Player1CharacterRuleWin
            },
            {
                new PlayerRoundInfo
                {
                    Character = Character.Deceiver,
                    Hand = new(){ Sheild = 1 },
                    ChosenCards = _CCC
                },
                new PlayerRoundInfo
                {
                    Character = Character.Deceiver,
                    Hand = new() { Sheild = 2},
                    ChosenCards = _CCC
                },
                Result.Player2CharacterRuleWin
            },
            {
                new PlayerRoundInfo
                {
                    Character = Character.Deceiver,
                    Hand = new() { Sheild = 2 },
                    ChosenCards = _CCC
                },
                new PlayerRoundInfo
                {
                    Character = Character.Deceiver,
                    Hand = new() { Sheild = 2},
                    ChosenCards = _CCC
                },
                Result.Draw
            },
            {
                new PlayerRoundInfo
                {
                    Character = Character.Deceiver,
                    Hand = new(),
                    ChosenCards = _SCC
                },
                new PlayerRoundInfo
                {
                    Character = Character.Deceiver,
                    Hand = new(),
                    ChosenCards = _SCC
                },
                Result.Draw
            },
            {
                new PlayerRoundInfo
                {
                    Character = Character.Deceiver,
                    Hand = new(),
                    ChosenCards = _CCC
                },
                new PlayerRoundInfo
                {
                    Character = Character.Deceiver,
                    Hand = new(),
                    ChosenCards = _SCC
                },
                Result.Player1Win
            },
            {
                new PlayerRoundInfo
                {
                    Character = Character.Deceiver,
                    Hand = new(),
                    ChosenCards = _SCC
                },
                new PlayerRoundInfo
                {
                    Character = Character.Deceiver,
                    Hand = new(),
                    ChosenCards = _DCC
                },
                Result.Player1Win
            },
            {
                new PlayerRoundInfo
                {
                    Character = Character.Deceiver,
                    Hand = new(),
                    ChosenCards = _DCC
                },
                new PlayerRoundInfo
                {
                    Character = Character.Deceiver,
                    Hand = new(),
                    ChosenCards = _CCC
                },
                Result.Player1Win
            },
            {
                new PlayerRoundInfo
                {
                    Character = Character.Deceiver,
                    Hand = new(),
                    ChosenCards = _CCC
                },
                new PlayerRoundInfo
                {
                    Character = Character.Deceiver,
                    Hand = new(),
                    ChosenCards = _DCC
                },
                Result.Player2Win
            },
            {
                new PlayerRoundInfo
                {
                    Character = Character.Deceiver,
                    Hand = new(),
                    ChosenCards = _SCC
                },
                new PlayerRoundInfo
                {
                    Character = Character.Deceiver,
                    Hand = new(),
                    ChosenCards = _CCC
                },
                Result.Player2Win
            },
            {
                new PlayerRoundInfo
                {
                    Character = Character.Deceiver,
                    Hand = new(),
                    ChosenCards = _DCC
                },
                new PlayerRoundInfo
                {
                    Character = Character.Deceiver,
                    Hand = new(),
                    ChosenCards = _SCC
                },
                Result.Player2Win
            },
            {
                new PlayerRoundInfo
                {
                    Character = Character.Deceiver,
                    Hand = new() { Sheild = 1 },
                    ChosenCards = _SCC
                },
                new PlayerRoundInfo
                {
                    Character = Character.Deceiver,
                    Hand = new() { Sheild = 2 },
                    ChosenCards = _SCC
                },
                Result.Player1CharacterRuleWin
            },
            {
                new PlayerRoundInfo
                {
                    Character = Character.Deceiver,
                    Hand = new() { Sheild = 2 },
                    ChosenCards = _SCC
                },
                new PlayerRoundInfo
                {
                    Character = Character.Deceiver,
                    Hand = new() { Sheild = 1 },
                    ChosenCards = _SCC
                },
                Result.Player2CharacterRuleWin
            },
            {
                new PlayerRoundInfo
                {
                    Character = Character.Deceiver,
                    Hand = new() { Sheild = 2 },
                    ChosenCards = _SCC
                },
                new PlayerRoundInfo
                {
                    Character = Character.Deceiver,
                    Hand = new() { Sheild = 2 },
                    ChosenCards = _SCC
                },
                Result.Draw
            },
            {
                new PlayerRoundInfo
                {
                    Character = Character.Deceiver,
                    Hand = new() { Sheild = 1 },
                    ChosenCards = _DCC
                },
                new PlayerRoundInfo
                {
                    Character = Character.Deceiver,
                    Hand = new() { Sheild = 2 },
                    ChosenCards = _DCC
                },
                Result.Player1CharacterRuleWin
            },
                {
                new PlayerRoundInfo
                {
                    Character = Character.Deceiver,
                    Hand = new() { Sheild = 2 },
                    ChosenCards = _DCC
                },
                new PlayerRoundInfo
                {
                    Character = Character.Deceiver,
                    Hand = new() { Sheild = 1 },
                    ChosenCards = _DCC
                },
                Result.Player2CharacterRuleWin
            },
            {
                new PlayerRoundInfo
                {
                    Character = Character.Deceiver,
                    Hand = new() { Sheild = 2 },
                    ChosenCards = _DCC
                },
                new PlayerRoundInfo
                {
                    Character = Character.Deceiver,
                    Hand = new() { Sheild = 2 },
                    ChosenCards = _DCC
                },
                Result.Draw
            },
        };

        static RuleTests()
        {
            var services = new ServiceCollection();

            // Register the AssassinRule service
            services.AddTransient<IGameService, GameService>();

            // Register mocks for dependencies (if any)
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddDbContext<AppDbContext>(options =>
                options.UseMongoDB(GetMongo(), "BoardGameDB"), ServiceLifetime.Scoped);

            ServiceProvider = services.BuildServiceProvider();
        }

        private static string GetMongo()
        {
            return $"mongodb+srv://{UserName}:{Password}@cluster0.r3hywvh.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
        }

        private static string UserName { get => "Maru"; }
        private static string Password { get => "13011821"; }

        public class AssassinRuleTests
        {
            [Theory]
            [MemberData(nameof(AssassinRuleData), MemberType = typeof(RuleTests))]
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

        public class DeceiverRuleTests
        {
            [Theory]
            [MemberData(nameof(DeceiverRuleData), MemberType = typeof(RuleTests))]
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
