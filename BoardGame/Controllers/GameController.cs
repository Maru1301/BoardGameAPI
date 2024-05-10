﻿using BoardGame.Authorizations;
using BoardGame.Infrastractures;
using BoardGame.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static BoardGame.Models.DTOs.GameDTOs;
using static BoardGame.Models.ViewModels.GameVMs;

namespace BoardGame.Controllers
{
    [AuthorizeRoles(Role.Member, Role.Guest)]
    [ApiController]
    [Route("api/[controller]")]
    public class GameController(IGameService gameService) : ControllerBase
    {
        private readonly IGameService _gameService = gameService;

        [HttpPost("[action]")]
        public async Task<IActionResult> BeginNewGame(GameInfoVM vm)
        {
            try
            {
                var user = HttpContext.User;

                // Extract the user Account from the JWT claim
                string userAccount = user.Identity?.Name ?? string.Empty;

                if (string.IsNullOrEmpty(userAccount)) return BadRequest("Invalid Account!");

                var Id = await _gameService.BeginNewGame(vm.To<GameInfoDTO>(), userAccount);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
            }

        [HttpGet("[action]")]
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

        [HttpPost("[action]")]
        public async Task<IActionResult> BeginNewRound(RoundInfoVM vm)
        {
            try
            {
                await _gameService.BeginNewRound(vm.To<RoundInfoDTO>());

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> OpenNextCard()
        {
            //var rule = _gameService.MapRule(ruleCharacter);

            throw new NotImplementedException();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> EndRound(Character playerChosenCharacter)
        {
            throw new NotImplementedException();
        }
    }
}
