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
        private Func<PlayerInfoContainer, PlayerInfoContainer, Result> _useRule;

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

        public bool? IsPlayerWin(Result result)
        {
            int resultNum = (int)result;
            if(resultNum % 2 == 0)
            {
                return true;
            }

            return false;
        }

        public void BeginNewGame()
        {
            _playerGoFirst = IsPlayerGoFirst();
            
        }

        public void BeginNewRound()
        {
            _useRule = _playerGoFirst ? _player.Character.UseRuleLogic : _npc.Character.UseRuleLogic;
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

            int chosenCard = random.Next(canChooseCards.Count);

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

            int chosenCard = random.Next(canChooseCards.Count);

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

        public void EndRound()
        {
            _playerGoFirst = !_playerGoFirst;
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
