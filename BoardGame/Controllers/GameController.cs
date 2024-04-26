using BoardGame.Authorizations;
using BoardGame.Infrastractures;
using BoardGame.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
        public NewGameInfoVM BeginNewGame([FromQuery] CharacterSet player, [FromQuery] CharacterSet? bot = null)
        {
            //determine who goes first
            var random = new Random().Next(1);
            var whoGoesFirst = (WhoGoesFirst)random;
            return new NewGameInfoVM()
            {
                WhoGoesFirst = whoGoesFirst,
            };
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
