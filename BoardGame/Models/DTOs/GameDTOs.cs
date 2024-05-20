using BoardGame.Infrastractures;
using BoardGame.Models.EFModels;
using MongoDB.Bson;

namespace BoardGame.Models.DTOs
{
    public class GameDTOs
    {
        public class GameDTO
        {
            public ObjectId Id { get; set; }
            public string Player1Account { get; set; } = string.Empty;
            public string Player2Account { get; set; } = string.Empty;
            public CharacterSet Player1Characters { get; set; } = new();
            public CharacterSet Player2Characters { get; set; } = new();
            public Round? Round1 { get; set; }
            public Round? Round2 { get; set; }
            public Round? Round3 { get; set; }
            public Round? Round4 { get; set; }
            public Round? Round5 { get; set; }
            public Round? Round6 { get; set; }
            public EndGameInfo EndGameInfo { get; set; } = new();
            public long CreatedTime { get; set; }
        }

        public class GameInfoRequestDTO
        {
            public string Player1Account { get; set; } = string.Empty;
            public string Player2Account { get; set; } = string.Empty;
            public CharacterSet Player1Characters { get; set; } = new();
            public CharacterSet Player2Characters { get; set; } = new();
        }

        public class GameInfoDTO
        {
            public string Player1Account { get; set; } = string.Empty;
            public string Player2Account { get; set; } = string.Empty;
            public CharacterSet Player1Characters { get; set; } = new();
            public CharacterSet Player2Characters { get; set; } = new();
            public long CreatedTime { get; set; } = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
        }

        public class RoundInfoRequestDTO
        {
            public ObjectId Id { get; set; }
            public PlayerRoundInfo Player1 { get; set; } = new();
            public PlayerRoundInfo Player2 { get; set; } = new();
        }

        public class RoundInfoDTO
        {
            public string Winner { get; set; } = string.Empty;

            //0: player first, 1: bot first
            public WhoGoesFirst WhoGoesFirst { get; set; }

            public PlayerRoundInfo Player1 { get; set; } = new();

            public PlayerRoundInfo Player2 { get; set; } = new();

            public long RoundStart { get; set; }

            public long RoundEnd { get; set; }

            public Character RuleCharacter  => WhoGoesFirst == WhoGoesFirst.Player1 ? Player1.Character : Player2.Character;
        }
    }
}
