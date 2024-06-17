using BoardGame.Authorizations;
using BoardGame.Infrastractures;
using BoardGame.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using static BoardGame.Models.DTOs.GameDTOs;
using Utility;

namespace BoardGame.Controllers
{
    [AuthorizeRoles(Role.Member, Role.Guest, Role.Admin)]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GameController(IGameService gameService) : ControllerBase
    {
        private readonly IGameService _gameService = gameService;
        
        [HttpGet, AuthorizeRoles(Role.Admin)]
        public async Task<IActionResult> GetGameList()
        {
            try
            {
                var games = await _gameService.GetGameList();

                return Ok(games);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BeginNewGame(GameInfoRequestDTO request)
        {
            try
            {
                string userAccount = GetUserAccount();

                var Id = await _gameService.BeginNewGame(request.To<GameInfoDTO>(), userAccount);

                return Ok(Id);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EndGame()
        {
            try
            {

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> BeginNewRound(RoundInfoRequestDTO request)
        {
            try
            {
                await _gameService.BeginNewRound(request.To<RoundInfoDTO>());

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> OpenNextCard()
        {
            try
            {
                //todo: make sure that the request is sent by the correct player
                string userAccount = GetUserAccount();

                return Ok(userAccount);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private string GetUserAccount()
        {
            string userAccount = HttpContext.GetJwtClaim(ClaimTypes.Name).Value;

            if (string.IsNullOrEmpty(userAccount))
            {
                throw new Exception("Invalid Account!");
            }

            return userAccount;
        }

        [HttpGet]
        public async Task<IActionResult> EndRound(Character playerChosenCharacter)
        {
            //todo: make sure that the request is sent by the correct player
            throw new NotImplementedException();
        }
    }
}
