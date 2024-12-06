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
        private static readonly List<Card> _CCC = [Card.Crown, Card.Crown, Card.Crown];

        private static readonly List<Card> _SCC = [Card.Sheild, Card.Crown, Card.Crown];

        private static readonly List<Card> _DCC = [Card.Dagger, Card.Crown, Card.Crown];

        public static TheoryData<PlayerRoundInfo, PlayerRoundInfo, GameResult> AssassinRuleData => new()
        {
            {
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new(),
                    ChosenCards = _CCC,
                },
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new(),
                    ChosenCards = _CCC
                },
                GameResult.Draw
            },
            {
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new(),
                    ChosenCards = _SCC
                },
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new(),
                    ChosenCards = _SCC
                },
                GameResult.Draw
            },
            {
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new(),
                    ChosenCards = _CCC
                },
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new(),
                    ChosenCards = _SCC
                },
                GameResult.Player1Win
            },
            {
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new(),
                    ChosenCards = _SCC
                },
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new(),
                    ChosenCards = _DCC
                },
                GameResult.Player1Win
            },
            {
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new(),
                    ChosenCards = _DCC
                },
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new(),
                    ChosenCards = _CCC
                },
                GameResult.Player1Win
            },
            {
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new(),
                    ChosenCards = _DCC
                },
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new(),
                    ChosenCards = _DCC
                },
                GameResult.Draw
            },
            {
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new(),
                    ChosenCards = _CCC
                },
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new(),
                    ChosenCards = _DCC
                },
                GameResult.Player2Win
            },
            {
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new(),
                    ChosenCards = _DCC
                },
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new(),
                    ChosenCards = _SCC
                },
                GameResult.Player2Win
            },
            {
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new(),
                    ChosenCards = _DCC
                },
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new(),
                    ChosenCards = _SCC
                },
                GameResult.Player2Win
            },
            {
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new(),
                    ChosenCards = _SCC
                },
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new(),
                    ChosenCards = _CCC
                },
                GameResult.Player2Win
            },
            {
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new(),
                    ChosenCards = _SCC
                },
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new(),
                    ChosenCards = _CCC
                },
                GameResult.Player2Win
            },
            {
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new(),
                    ChosenCards = _SCC
                },
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new(),
                    ChosenCards = _CCC
                },
                GameResult.Player2Win
            },
            {
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new() { Dagger = 3},
                    ChosenCards = _DCC
                },
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new() { Dagger = 2 },
                    ChosenCards = _DCC
                },
                GameResult.Player1CharacterRuleWin
            },
            {
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new() { Dagger = 2},
                    ChosenCards = _DCC
                },
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new() { Dagger = 3 },
                    ChosenCards = _DCC
                },
                GameResult.Player2CharacterRuleWin
            },
            {
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new() { Dagger = 2},
                    ChosenCards = _DCC
                },
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new() { Dagger = 2 },
                    ChosenCards = _DCC
                },
                GameResult.Draw
            },
            {
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new() { Dagger = 2},
                    ChosenCards = _DCC
                },
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new() { Dagger = 2 },
                    ChosenCards = _DCC
                },
                GameResult.Draw
            },
            {
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new() { Dagger = 2},
                    ChosenCards = _DCC
                },
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new() { Dagger = 1 },
                    ChosenCards = _DCC
                },
                GameResult.Draw
            },
            {
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new() { Dagger = 1},
                    ChosenCards = _DCC
                },
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Assassin,
                    Hand = new() { Dagger = 2 },
                    ChosenCards = _DCC
                },
                GameResult.Draw
            }
        };

        public static TheoryData<PlayerRoundInfo, PlayerRoundInfo, GameResult> DeceiverRuleData => new()
        {
            {
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Deceiver,
                    Hand = new() { Sheild = 2 },
                    ChosenCards = _CCC
                },
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Deceiver,
                    Hand = new() { Sheild = 1},
                    ChosenCards = _CCC
                },
                GameResult.Player1CharacterRuleWin
            },
            {
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Deceiver,
                    Hand = new(){ Sheild = 1 },
                    ChosenCards = _CCC
                },
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Deceiver,
                    Hand = new() { Sheild = 2},
                    ChosenCards = _CCC
                },
                GameResult.Player2CharacterRuleWin
            },
            {
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Deceiver,
                    Hand = new() { Sheild = 2 },
                    ChosenCards = _CCC
                },
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Deceiver,
                    Hand = new() { Sheild = 2},
                    ChosenCards = _CCC
                },
                GameResult.Draw
            },
            {
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Deceiver,
                    Hand = new(),
                    ChosenCards = _SCC
                },
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Deceiver,
                    Hand = new(),
                    ChosenCards = _SCC
                },
                GameResult.Draw
            },
            {
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Deceiver,
                    Hand = new(),
                    ChosenCards = _CCC
                },
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Deceiver,
                    Hand = new(),
                    ChosenCards = _SCC
                },
                GameResult.Player1Win
            },
            {
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Deceiver,
                    Hand = new(),
                    ChosenCards = _SCC
                },
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Deceiver,
                    Hand = new(),
                    ChosenCards = _DCC
                },
                GameResult.Player1Win
            },
            {
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Deceiver,
                    Hand = new(),
                    ChosenCards = _DCC
                },
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Deceiver,
                    Hand = new(),
                    ChosenCards = _CCC
                },
                GameResult.Player1Win
            },
            {
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Deceiver,
                    Hand = new(),
                    ChosenCards = _CCC
                },
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Deceiver,
                    Hand = new(),
                    ChosenCards = _DCC
                },
                GameResult.Player2Win
            },
            {
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Deceiver,
                    Hand = new(),
                    ChosenCards = _SCC
                },
                new PlayerRoundInfo
            {
                    Character = BoardGame.Infrastractures.Character.Deceiver,
                    Hand = new(),
                    ChosenCards = _CCC
                },
                GameResult.Player2Win
            },
            {
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Deceiver,
                    Hand = new(),
                    ChosenCards = _DCC
                },
                new PlayerRoundInfo
            {
                    Character = BoardGame.Infrastractures.Character.Deceiver,
                    Hand = new(),
                    ChosenCards = _SCC
                },
                GameResult.Player2Win
            },
            {
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Deceiver,
                    Hand = new() { Sheild = 1 },
                    ChosenCards = _SCC
                },
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Deceiver,
                    Hand = new() { Sheild = 2 },
                    ChosenCards = _SCC
                },
                GameResult.Player1CharacterRuleWin
            },
            {
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Deceiver,
                    Hand = new() { Sheild = 2 },
                    ChosenCards = _SCC
                },
                new PlayerRoundInfo
            {
                    Character = BoardGame.Infrastractures.Character.Deceiver,
                    Hand = new() { Sheild = 1 },
                    ChosenCards = _SCC
                },
                GameResult.Player2CharacterRuleWin
            },
            {
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Deceiver,
                    Hand = new() { Sheild = 2 },
                    ChosenCards = _SCC
                },
                new PlayerRoundInfo
            {
                    Character = BoardGame.Infrastractures.Character.Deceiver,
                    Hand = new() { Sheild = 2 },
                    ChosenCards = _SCC
                },
                GameResult.Draw
            },
            {
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Deceiver,
                    Hand = new() { Sheild = 1 },
                    ChosenCards = _DCC
                },
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Deceiver,
                    Hand = new() { Sheild = 2 },
                    ChosenCards = _DCC
                },
                GameResult.Player1CharacterRuleWin
            },
                {
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Deceiver,
                    Hand = new() { Sheild = 2 },
                    ChosenCards = _DCC
                },
                new PlayerRoundInfo
            {
                    Character = BoardGame.Infrastractures.Character.Deceiver,
                    Hand = new() { Sheild = 1 },
                    ChosenCards = _DCC
                },
                GameResult.Player2CharacterRuleWin
            },
            {
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Deceiver,
                    Hand = new() { Sheild = 2 },
                    ChosenCards = _DCC
                },
                new PlayerRoundInfo
                {
                    Character = BoardGame.Infrastractures.Character.Deceiver,
                    Hand = new() { Sheild = 2 },
                    ChosenCards = _DCC
                },
                GameResult.Draw
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

        [Theory]
        [MemberData(nameof(AssassinRuleData), MemberType = typeof(RuleTests))]
        [MemberData(nameof(DeceiverRuleData), MemberType = typeof(RuleTests))]
        public void TestRule_NormalRule(PlayerRoundInfo player1, PlayerRoundInfo player2, GameResult expected)
        {
            // Get the service instance from the static field
            var service = ServiceProvider.GetService<IGameService>();

            // Act
            var result = service!.MapRule(player1.Character)(player1, player2);

            // Assert
            Assert.Equal(result, expected);
        }

        [Fact]
        public void TestRule_InvalidCardCombination()
        {
            // Get the service instance from the static field
            var service = ServiceProvider.GetService<IGameService>();

            // Arrange
            var player1 = new PlayerRoundInfo
            {
                Character = BoardGame.Infrastractures.Character.Assassin,
                Hand = new(),
                ChosenCards = [(Card)100, Card.Crown, Card.Crown],
                LastOpened = 0,
            };
            var player2 = new PlayerRoundInfo
            {
                Character = BoardGame.Infrastractures.Character.Assassin,
                Hand = new(),
                ChosenCards = [(Card)100, Card.Crown, Card.Crown],
                LastOpened = 0,
            };

            // Act and Assert
            var exception = Assert.Throws<Exception>(() => service!.MapRule(player1.Character)(player1, player2));
            Assert.Equal("Invalid card combination", exception.Message);
        }
    }
}
