using Menu_Practice.Characters.Builders;

namespace Menu_Practice.Characters
{
    internal class CharacterList
    {
        public Dictionary<string, Character> Characters { get; } = new();

        public CharacterList()
        {
            //build Character "Lord"
            var characterName = Resources.領主;
            var character = BuildCharacter(new LordBuilder());
            Characters[characterName] = character;

            //build Character "Knight"
            characterName = Resources.騎士;
            character = BuildCharacter(new KnightBuilder());
            Characters[characterName] = character;

            //build Character "Soldier"
            characterName = Resources.士兵;
            character = BuildCharacter(new SoldierBuilder());
            Characters[characterName] = character;

            //build Character "Lobbyist"
            characterName = Resources.說客;
            character = BuildCharacter(new LobbyistBuilder());
            Characters[characterName] = character;

            //build Character "Assassin"
            characterName = Resources.刺客;
            character = BuildCharacter(new AssassinBuilder());
            Characters[characterName] = character;

            //build Character "Deceiver"
            characterName = Resources.詐欺師_;
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
