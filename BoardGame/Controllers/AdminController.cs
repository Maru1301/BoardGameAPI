using BoardGame.Infrastractures;
using BoardGame.Models.DTOs;
using static BoardGame.Models.ViewModels.AdminVMs;
using BoardGame.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BoardGame.Services;
using System.Net;
using BoardGame.Authorizations;

namespace BoardGame.Controllers
{
    [AuthorizeRoles(Roles.Admin)]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController(IAdminService adminService, JWTHelper jwt) : ControllerBase
    {
        private readonly IAdminService _adminService = adminService;

        private readonly JWTHelper _jwt = jwt;

        [HttpPost("[action]")]
        public async Task<IActionResult> AddAdmin(AdminCreateVM vm)
        {
            try
            {
                string result = await _adminService.AddAdmin(vm.ToDTO<AdminCreateDTO>());

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
        public async Task<IActionResult> Login([FromQuery] LoginVM vm)
        {
            try
            {
                var role = await _adminService.ValidateUser(vm.ToDTO<LoginDTO>());
                
                // Authorize the user and generate a JWT token.
                var token = _jwt.GenerateToken(vm.Account, role);

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
