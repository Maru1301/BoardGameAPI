using Menu_Practice.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Menu_Practice.Program;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Menu_Practice
{
    internal class Game
    {
        private readonly Player _player = new();
        private readonly Player _npc = new();
        public Game(Character character, Character opponent)
        {
            _player.Character = character;
            _npc.Character = opponent;
        }
        public Status Start()
        {
            //who go first
            var random = new Random();
            int WhoGoFirst = random.Next(2);
            if(WhoGoFirst == 1)
            {
                _player.GoFirst = true;
                _npc.GoFirst = false;
            }
            else
            {
                _player.GoFirst = false;
                _npc.GoFirst = true;
            }
            
            return Status.InMenu;
        }

        private class Player
        {
            public Character Character { get; set; } = new();

            public bool GoFirst { get; set; }
        }

        public delegate (bool, OutComeCallback) RunRule(int card1, int card2);

        public delegate void OutComeCallback();

        public class Round
        {
            private int _playerChosenCard;
            private int _npcChosenCard;
            private RunRule _runRule;

            public Round(RunRule callback)
            {
                _runRule = callback;
            }

            internal void ChooseCard(List<int> playerCards, List<int> npcCards)
            {
                for(int i = 0; i < playerCards.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            Console.WriteLine($"皇冠x{playerCards[i]}");
                            break;
                        case 1:
                            Console.WriteLine($"盾牌x{playerCards[i]}");
                            break;
                        case 2:
                            Console.WriteLine($"匕首x{playerCards[i]}");
                            break;
                    }
                }
                Console.ReadKey();
            }

            internal void Judge(int playerChosenCard, int npcChosenCard)
            {
                (bool playerWin, OutComeCallback outComeCallback) = _runRule(playerChosenCard, npcChosenCard);

                outComeCallback();
            }
        }
    }
}
