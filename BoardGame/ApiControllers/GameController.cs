using BoardGame.Authorizations;
using BoardGame.Infrastractures;
using BoardGame.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BoardGame.ApiControllers
{
    [AuthorizeRoles(Role.Member, Role.Guest, Role.Admin)]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GameController(IGameService gameService) : ControllerBase
    {
        private readonly IGameService _gameService = gameService;

        [HttpGet, AuthorizeRoles(Role.Admin)]
        public async Task<IActionResult> GetGameRecordList()
        {
            try
            {
                var games = await _gameService.GetGameRecordList();

                return Ok(games);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet, /*AuthorizeRoles(Role.Member, Role.Guest)*/ AllowAnonymous]
        public async Task<IActionResult> GetCharacterList()
        {
            try
            {
                var games = await _gameService.GetCharacterList();

                return Ok(games);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
