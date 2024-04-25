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
        private int _endGame = 5;
        private Func<PlayerInfoContainer, PlayerInfoContainer, Result> _useRule;

        public GameController(Character character, Character opponent)
        {
            _player.Character = new(character);
            _npc.Character = new(opponent);
        }

        public void BeginNewGame()
        {
            _playerGoFirst = IsPlayerGoFirst();
            _roundCount = 0;
        }

        private bool IsPlayerGoFirst()
        {
            //who go first
            var random = new Random();
            int WhoGoFirst = random.Next(1);
            int PlayerGoFirst = 1;

            return WhoGoFirst == PlayerGoFirst;
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

        public List<int> GetNPCCards()
        {
            return _npc.Character.Cards;
        }

        public int GetNPCChosenCard()
        {
            Random random = new();
            List<int> npcCards = _npc.Character.Cards;

            List<int> canChooseCards = new();
            foreach(var item in npcCards.Select((cardAmount, index) => new { index, cardAmount }))
            {
                if(item.cardAmount > 0)
                {
                    canChooseCards.Add(item.index);
                }
            }

            int chosenCard = canChooseCards[random.Next(canChooseCards.Count)];

            return chosenCard;
        }

        public Result JudgeRound(PlayerInfoContainer playerInfo, PlayerInfoContainer ncpInfo)
        {
            Result result = _useRule(playerInfo, ncpInfo);

            return result;
        }

        public Card GetNPCWinCard()
        {
            Random random = new();
            List<int> playerCards = _player.Character.Cards;

            List<int> canChooseCards = new();
            foreach (var item in playerCards.Select((cardAmount, index) => new { index, cardAmount }))
            {
                if (item.cardAmount > 0)
                {
                    canChooseCards.Add(item.index);
                }
            }

            int chosenCard = canChooseCards[random.Next(canChooseCards.Count)];

            return (Card)chosenCard;
        }

        public void ProcessSettlement(Result result, Card card)
        {
            bool? playerWin = IsPlayerWin(result);

            if(playerWin == null)
            {
                return;
            }
            else if(playerWin == true)
            {
                _player.Character.Cards[(int)card]++;
                _npc.Character.Cards[(int)card]--;
            }
            else
            {
                _npc.Character.Cards[(int)card]++;
                _player.Character.Cards[(int)card]--;
            }
        }

        private bool? IsPlayerWin(Result result)
        {
            if (result.Equals(Result.Draw)) return null;

            int resultNum = (int)result;
            if (resultNum % 2 == 0)
            {
                return true;
            }

            return false;
        }

        public Status EndRound()
        {
            if (_roundCount == _endGame) return Status.InMenu;

            _playerGoFirst = !_playerGoFirst;

            return Status.InGame;
        }

        public string GetOutcome()
        {
            int playerPoint = _player.Character.PointLogic();
            int npcPoint = _npc.Character.PointLogic();

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

class PlayerInfoContainer
{
    public List<int> Cards = new();

    public int ChosenCard;

    public PlayerInfoContainer(List<int> Cards, int ChosenCard)
    {
        this.Cards = Cards;
        this.ChosenCard = ChosenCard;
    }
}
