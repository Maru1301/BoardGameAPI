using Menu_Practice.Characters.Builders;

namespace Menu_Practice.Characters
{
    internal class CharacterList
    {
        public Dictionary<string, Character> Characters { get; } = new();

        public CharacterList()
        {
            //build Character "Lord"
            var characterName = "領主";
            var character = BuildCharacter(new LordBuilder());
            Characters[characterName] = character;

            //build Character "Knight"
            characterName = "騎士";
            character = BuildCharacter(new KnightBuilder());
            Characters[characterName] = character;

            //build Character "Soldier"
            characterName = "士兵";
            character = BuildCharacter(new SoldierBuilder());
            Characters[characterName] = character;

            //build Character "Lobbyist"
            characterName = "說客";
            character = BuildCharacter(new LobbyistBuilder());
            Characters[characterName] = character;

            //build Character "Assassin"
            characterName = "刺客";
            character = BuildCharacter(new AssassinBuilder());
            Characters[characterName] = character;

            //build Character "Deceiver"
            characterName = "詐欺師*";
            character = BuildCharacter(new DeceiverBuilder());
            Characters[characterName] = character;
        }

        private static Character BuildCharacter(ICharacterBuilder builderType)
        {
            var builder = builderType;
            CharacterDirector director = new(ref builder);
            director.ConstructCharacter();
            var character = builder.GetCharacter();
            
            return character;
        }
    }
}
