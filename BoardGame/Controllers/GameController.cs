using BoardGame.Authorizations;
using BoardGame.Infrastractures;
using BoardGame.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static BoardGame.Models.DTOs.GameDTOs;
using static BoardGame.Models.ViewModels.GameVMs;

namespace BoardGame.Controllers
{
    [AuthorizeRoles(Roles.Member)]
    [ApiController]
    [Route("api/[controller]")]
    public class GameController(IGameService gameService) : ControllerBase
    {
        private readonly IGameService _gameService = gameService;

        [HttpPost("[action]")]
        public IActionResult BeginNewGame(GameInfoVM gameInfo)
        {
            try
            {
                var user = HttpContext.User;

                // Extract the user ID (or other relevant claim) from the JWT claim
                string userAccount = user.Identity?.Name ?? string.Empty;

                if (string.IsNullOrEmpty(userAccount)) return BadRequest("Invalid Account!");
                gameInfo.Account = userAccount;
                gameInfo.CreatedTime = DateTime.UtcNow;

                _gameService.BeginNewGame(gameInfo.ToDTO<GameInfoDTO>());

                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        [HttpGet("[action]")]
        public IActionResult BeginNewRound(Character playerChosenCharacter)
        {
            var rule = _gameService.MapRule(playerChosenCharacter);

            return Ok();
        }

        [HttpGet("[action]")]
        public IActionResult OpenNextCard()
        {
            throw new NotImplementedException();
        }

        [HttpGet("[action]")]
        public IActionResult EndRound(Character playerChosenCharacter)
        {
            throw new NotImplementedException();
        }
    }

    public class CharacterSet
    {
        public Character Character1 { get; set; }

        public Character Character2 { get; set; }

        public Character Characetr3 { get; set; }
    }
}
