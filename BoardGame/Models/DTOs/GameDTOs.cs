using BoardGame.Controllers;
using BoardGame.Infrastractures;
using BoardGame.Models.EFModels;

namespace BoardGame.Models.DTOs
{
    public class GameDTOs
    {
        public class GameInfoDTO
        {
            public string Player1Account { get; set; } = string.Empty;
            public string Player2Account { get; set; } = string.Empty;
            public CharacterSet Player1Characters { get; set; } = new();
            public CharacterSet Player2Characters { get; set; } = new();
            public long CreatedTime { get; set; } = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
        }

        public class RoundDTO
        {
            public string Winner { get; set; } = string.Empty;

            //0: player first, 1: bot first
            public WhoGoesFirst WhoGoesFirst { get; set; }

            public PlayerInfo Player1 { get; set; } = new();

            public PlayerInfo Player2 { get; set; } = new();

            public long RoundStart { get; set; }

            public long RoundEnd { get; set; }

            public Character RuleCharacter { get => WhoGoesFirst == WhoGoesFirst.Player1 ? Player1.Character : Player2.Character; }
        }
    }
}
