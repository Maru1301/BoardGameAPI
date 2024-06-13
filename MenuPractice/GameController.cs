using Menu_Practice.Characters;
using static Menu_Practice.Program;

namespace Menu_Practice
{
    internal class GameController
    {
        private readonly Player _player = new();
        private readonly Player _npc = new();
        private bool _playerGoFirst;
        private int _roundCount;
        private const int EndGame = 5;
        private Func<PlayerInfoContainer, PlayerInfoContainer, Result>? _useRule;

        public GameController(Character character, Character opponent)
        {
            _player.Character = new Character(character);
            _npc.Character = new Character(opponent);
        }

        public void BeginNewGame()
        {
            _playerGoFirst = IsPlayerGoFirst();
            _roundCount = 0;
        }

        private static bool IsPlayerGoFirst()
        {
            //who go first
            var random = new Random();
            var whoGoFirst = random.Next(1);
            const int playerGoFirst = 1;

            return whoGoFirst == playerGoFirst;
        }

        public void BeginNewRound()
        {
            _useRule = _playerGoFirst ? _player.Character.UseRuleLogic : _npc.Character.UseRuleLogic;
            _roundCount++;
        }

        public List<int> GetPlayerCards()
        {
            return _player.Character.Cards;
        }

        public List<int> GetNpcCards()
        {
            return _npc.Character.Cards;
        }

        public int GetNpcChosenCard()
        {
            Random random = new();
            var npcCards = _npc.Character.Cards;

            var canChooseCards = (from item in npcCards.Select((cardAmount, index) => new { index, cardAmount }) where item.cardAmount > 0 select item.index).ToList();

            var chosenCard = canChooseCards[random.Next(canChooseCards.Count)];

            return chosenCard;
        }

        public Result JudgeRound(PlayerInfoContainer playerInfo, PlayerInfoContainer ncpInfo)
        {
            var result = _useRule!(playerInfo, ncpInfo);

            return result;
        }

        public Card GetNpcWinCard()
        {
            Random random = new();
            var playerCards = _player.Character.Cards;

            var canChooseCards = (from item in playerCards.Select((cardAmount, index) => new { index, cardAmount }) where item.cardAmount > 0 select item.index).ToList();

            var chosenCard = canChooseCards[random.Next(canChooseCards.Count)];

            return (Card)chosenCard;
        }

        public void ProcessSettlement(Result result, Card card)
        {
            var playerWin = IsPlayerWin(result);

            switch (playerWin)
            {
                case null:
                    return;
                case true:
                    _player.Character.Cards[(int)card]++;
                    _npc.Character.Cards[(int)card]--;
                    break;
                default:
                    _npc.Character.Cards[(int)card]++;
                    _player.Character.Cards[(int)card]--;
                    break;
            }
        }

        private static bool? IsPlayerWin(Result result)
        {
            if (result.Equals(Result.Draw)) return null;

            var resultNum = (int)result;
            return resultNum % 2 == 0;
        }

        public Status EndRound()
        {
            if (_roundCount == EndGame) return Status.InMenu;

            _playerGoFirst = !_playerGoFirst;

            return Status.InGame;
        }

        public static string GetOutcome()
        {
            const int playerPoint = 0;
            const int npcPoint = 0;

            if(playerPoint == npcPoint)
            {
                return "平手";
            }

            return playerPoint > npcPoint ? "勝利" : "敗北";
        }

        private class Player
        {
            public Character Character { get; set; } = new();

            public bool GoFirst { get; set; }
        }
    }
}

public class PlayerInfoContainer(List<int> cards, int chosenCard)
{
    public readonly List<int> Cards = cards;

    public readonly int ChosenCard = chosenCard;
}
