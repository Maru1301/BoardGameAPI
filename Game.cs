﻿using Menu_Practice.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu_Practice
{
    internal class Game
    {
        private Character _character;
        private Character _opponent;
        public Game(Character character, Character opponent)
        {
            _character = character;
            _opponent = opponent;
        }
        public void Start()
        {

        }
    }
}
