using BoardGame.Infrastractures;
using BoardGame.Models.DTO;
using BoardGame.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BoardGame.Services;
using BoardGame.Authorizations;
using Utility;

namespace BoardGame.ApiControllers
{
    [AuthorizeRoles(Role.Admin)]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AdminController(IAdminService adminService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddAdmin(AdminCreateRequestDTO vm)
        {
            try
            {
                string result = await adminService.AddAdmin(vm.To<AdminCreateDTO>());

                return Ok(result);
            }
            catch (AdminServiceException ex)
            {
                return BadRequest($"Add admin failed. Please check the provided information. {ex.Message}");
            }
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> Login([FromQuery] AdminLoginRequestDTO vm)
        {
            try
            {
                var token = await adminService.ValidateUser(vm.To<LoginRequestDTO>());

                return Ok(token);
            }
            catch (AdminServiceException ex)
            {
                return BadRequest($"Login failed. Please check the provided information. {ex.Message}");
            }
        }
    }
}
