using BoardGame.Infrastractures;
using BoardGame.Models.EFModels;
using MongoDB.Bson;

namespace BoardGame.Models.DTOs
{
    public class GameDTOs
    {
        // Base class for shared properties
        public class GameBaseDTO
        {
            public string Player1Account { get; set; } = string.Empty;
            public string Player2Account { get; set; } = string.Empty;
            public CharacterSet Player1Characters { get; set; } = new();
            public CharacterSet Player2Characters { get; set; } = new();
        }

        public class GameDTO : GameBaseDTO
        {
            public ObjectId Id { get; set; }
            public Round? Round1 { get; set; }
            public Round? Round2 { get; set; }
            public Round? Round3 { get; set; }
            public Round? Round4 { get; set; }
            public Round? Round5 { get; set; }
            public Round? Round6 { get; set; }
            public EndGameInfo EndGameInfo { get; set; } = new();
            public long CreatedTime { get; set; }
        }

        public class GameInfoRequestDTO : GameBaseDTO
        {
        }

        public class GameInfoDTO : GameBaseDTO
        {
            public long CreatedTime { get; set; } = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
        }

        // Other classes remain the same
        public class RoundInfoRequestDTO
        {
            public ObjectId GameId { get; set; }
            public PlayerRoundInfo Player1 { get; set; } = new();
            public PlayerRoundInfo Player2 { get; set; } = new();
        }

        public class RoundInfoDTO
        {
            public ObjectId GameId { get; set; }
            public string Winner { get; set; } = string.Empty;
            public WhoGoesFirst WhoGoesFirst { get; set; }
            public PlayerRoundInfo Player1 { get; set; } = new();
            public PlayerRoundInfo Player2 { get; set; } = new();
            public long RoundStart { get; set; }
            public long RoundEnd { get; set; }
            public Character RuleCharacter => WhoGoesFirst == WhoGoesFirst.Player1 ? Player1.Character : Player2.Character;
        }
    }
}
