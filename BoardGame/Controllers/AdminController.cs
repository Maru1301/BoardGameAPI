using BoardGame.Infrastractures;
using BoardGame.Models.DTOs;
using BoardGame.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BoardGame.Services;
using System.Net;
using BoardGame.Authorizations;
using Utility;

namespace BoardGame.Controllers
{
    [AuthorizeRoles(Role.Admin)]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AdminController(IAdminService adminService) : ControllerBase
    {
        private readonly IAdminService _adminService = adminService;

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAdmin(AdminCreateRequestDTO vm)
        {
            try
            {
                string result = await _adminService.AddAdmin(vm.To<AdminCreateDTO>());

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

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> Login([FromQuery] AdminLoginRequestDTO vm)
        {
            try
            {
                var token = await _adminService.ValidateUser(vm.To<LoginDTO>());

                return Ok(new { token });
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
