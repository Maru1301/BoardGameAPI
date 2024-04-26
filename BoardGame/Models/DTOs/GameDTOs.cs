using BoardGame.Controllers;

namespace BoardGame.Models.DTOs
{
    public class GameDTOs
    {
        public class GameInfoDTO
        {
            public string Account { get; set; } = string.Empty;

            public CharacterSet Player { get; set; } = new();

            public CharacterSet Bot { get; set; } = new();
        }
    }
}
