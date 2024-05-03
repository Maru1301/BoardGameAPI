using BoardGame.Controllers;

namespace BoardGame.Models.DTOs
{
    public class GameDTOs
    {
        public class GameInfoDTO
        {
            public string MemberAccount { get; set; } = string.Empty;

            public CharacterSet Player1Characters { get; set; } = new();

            public CharacterSet Player2Characters { get; set; } = new();
        }
    }
}
