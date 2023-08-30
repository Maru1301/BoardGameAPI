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

            while (true)
            {
                Round round;
                //todo start each round one by one
                if (_player.GoFirst)
                {
                    round = new(_player.Character.Rule);
                    _player.GoFirst = false;
                    _npc.GoFirst = true;
                }
                else
                {
                    round = new(_npc.Character.Rule);
                    _npc.GoFirst = false;
                    _player.GoFirst = true;
                }

                round.ChooseCard(_player.Character.Cards, _npc.Character.Cards);
            }
            
            return Status.InMenu;
        }

        private class Player
        {
            public Character Character { get; set; } = new();

            public bool GoFirst { get; set; }
        }

        private class Round
        {
            private int _playerChosenCard;
            private int _npcChosenCard;
            public Func<int> Rule { get; set; }

            public Round(Func<int> func)
            {
                Rule = func;
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
            }
        }
    }
}
