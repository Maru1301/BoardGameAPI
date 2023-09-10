using Menu_Practice.Characters;
using Menu_Practice.Characters.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Menu_Practice.GameController;
using static Menu_Practice.Program;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Menu_Practice
{
    internal class GameController
    {
        private readonly Player _player = new();
        private readonly Player _npc = new();
        private bool _playerGoFirst;
        public GameController(Character character, Character opponent)
        {
            _player.Character = character;
            _npc.Character = opponent;
        }
        public bool IsPlayerGoFirst()
        {
            //who go first
            var random = new Random();
            int WhoGoFirst = random.Next(1);
            int PlayerGoFirst = 1;

            return WhoGoFirst == PlayerGoFirst;
        }

        public void BeginNewGame()
        {
            _playerGoFirst = IsPlayerGoFirst();
        }

        public Round BeginNewRound()
        {
            Round round = _playerGoFirst ? new(_player.Character.UseRuleLogic) : new(_npc.Character.UseRuleLogic);
            _playerGoFirst = !_playerGoFirst;

            return round;
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
            Random random = new Random();
            List<int> npcCards = _npc.Character.Cards;

            List<int> canChooseCards = new List<int>();
            foreach(var item in npcCards.Select((cardAmount, index) => new { index, cardAmount }))
            {
                if(item.cardAmount > 0)
                {
                    canChooseCards.Add(item.index);
                }
            }

            int chosenCard = random.Next(canChooseCards.Count);

            return chosenCard;
        }

        private class Player
        {
            public Character Character { get; set; } = new();

            public bool GoFirst { get; set; }
        }

        public class Round
        {
            private int _playerChosenCard;
            private int _npcChosenCard;
            private Func<PlayerInfoContainer, PlayerInfoContainer, Result> _useRule;

            public Round(Func<PlayerInfoContainer, PlayerInfoContainer, Result> useRule)
            {
                _useRule = useRule;
            }

            internal void Judge(PlayerInfoContainer playerInfo, PlayerInfoContainer ncpInfo)
            {
                var result = _useRule(playerInfo, ncpInfo);

                if(result == Result.BasicWin)
                {

                }
                else if(result == Result.BasicLose)
                {

                }
            }
        }
    }
}

class PlayerInfoContainer
{
    public List<int> Cards = new List<int>();

    public int ChosenCard;

    public PlayerInfoContainer(List<int> Cards, int ChosenCard)
    {
        this.Cards = Cards;
        this.ChosenCard = ChosenCard;
    }
}
