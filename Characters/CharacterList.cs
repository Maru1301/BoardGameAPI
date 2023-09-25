using Menu_Practice.Characters.Builders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu_Practice.Characters
{
    internal class CharacterList
    {
        private readonly Dictionary<string, Character> _characters = new();

        public Dictionary<string, Character> Characters { get => _characters; }

        public CharacterList()
        {
            string characterName;
            Character character;

            //build Character "Lord"
            characterName = "領主";
            character = BuildCharacter(new LordBuilder());
            _characters[characterName] = character;

            //build Character "Knight"
            characterName = "騎士";
            character = BuildCharacter(new KnightBuilder());
            _characters[characterName] = character;

            //build Character "Soldier"
            characterName = "士兵";
            character = BuildCharacter(new SoldierBuilder());
            _characters[characterName] = character;

            //build Character "Lobbyist"
            characterName = "說客";
            character = BuildCharacter(new LobbyistBuilder());
            _characters[characterName] = character;

            //build Character "Assassin"
            characterName = "刺客";
            character = BuildCharacter(new AssassinBuilder());
            _characters[characterName] = character;

            //build Character "Deceiver"
            characterName = "詐欺師*";
            character = BuildCharacter(new DeceiverBuilder());
            _characters[characterName] = character;
        }

        private static Character BuildCharacter(ICharacterBuilder builderType)
        {
            ICharacterBuilder builder = builderType;
            CharacterDirector director = new(ref builder);
            director.ConstructChracter();
            Character character = builder.GetCharacter();
            
            return character;
        }
    }
}
