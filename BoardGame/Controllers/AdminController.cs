using BoardGame.Infrastractures;
using BoardGame.Models.DTOs;
using static BoardGame.Models.ViewModels.AdminVMs;
using BoardGame.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BoardGame.Services;
using System.Net;

namespace BoardGame.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        private readonly JWTHelper _jwt;

        public AdminController(IAdminService adminService, JWTHelper jwt)
        {
            _adminService = adminService;
            _jwt = jwt;
        }

        [HttpPost("[action]")]
        public IActionResult AddAdmin(AdminCreateVM createVM)
        {
            try
            {
                string result = _adminService.AddAdmin(createVM.ToDTO<AdminCreateDTO>());

                return Ok(result);
            }
            catch (AdminServiceException ex)
            {
                return BadRequest($"Add admin failed. Please check the provided information. {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("[action]"), AllowAnonymous]
        public async Task<IActionResult> Login([FromQuery] LoginVM login)
        {
            try
            {
                var role = await _adminService.ValidateUser(login.ToDTO<LoginDTO>());
                if (string.IsNullOrEmpty(role))
                {
                    return BadRequest("Invalid username or password.");
                }

                // Authorize the user and generate a JWT token.
                var token = _jwt.GenerateToken(login.Account, role);
                return Ok(token);
            }
            catch (AdminServiceException ex)
            {
                return BadRequest($"Login failed. Please check the provided information. {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
