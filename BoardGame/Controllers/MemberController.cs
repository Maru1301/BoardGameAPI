using BoardGame.Infrastractures;
using BoardGame.Models.DTOs;
using BoardGame.Services;
using BoardGame.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static BoardGame.Models.ViewModels.MemberVMs;

namespace BoardGame.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;

        private readonly JWTHelper _jwt;

        public MemberController(IMemberService memberService, JWTHelper jwt)
        {
            _memberService = memberService;
            _jwt = jwt;
        }

        [HttpGet("[action]"), Authorize("RequireAdmin")]
        public IActionResult ListMembers()
        {
            try
            {
                var members = _memberService.ListMembers();
                return Ok(members.Select(m => m.ToVM<MemberVM>()).ToList());
            }
            catch (MemberServiceException ex)
            {
                return BadRequest($"List failed. Please check the provided information. {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("[action]"), Authorize("RequireMember")]
        public IActionResult GetMemberInfo()
        {
            try
            {
                // Get the current user from the HttpContext
                var user = HttpContext.User;

                // Extract the user ID (or other relevant claim) from the JWT claim
                string userAccount = user.Identity?.Name ?? string.Empty;

                if(string.IsNullOrEmpty(userAccount)) return NotFound();

                // Use the user ID to retrieve member information from your database
                var member = _memberService.GetMemberInfo(userAccount);

                // Return the member information 
                return Ok(member);
            }
            catch (MemberServiceException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately, consider logging or returning error details
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("[action]"), AllowAnonymous]
        public async Task<IActionResult> Login([FromQuery] LoginVM login)
        {
            try
            {
                 var role = await _memberService.ValidateUser(login.ToDTO<LoginDTO>());
                if (string.IsNullOrEmpty(role))
                {
                    return BadRequest("Invalid username or password.");
                }

                // Authorize the user and generate a JWT token.
                var token = _jwt.GenerateToken(login.Account, role);
                return Ok(token);
            }
            catch(MemberServiceException ex)
            {
                return BadRequest($"Login failed. Please check the provided information. {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        
        [HttpPost("[action]"), AllowAnonymous]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            try
            {
                // Define a template for the confirmation email URL.
                string confirmationUrlTemplate = "https://localhost:44318/Member/ActivateRegistration";

                string Message = await _memberService.Register(register.ToDTO<RegisterDTO>(), confirmationUrlTemplate);

                return Ok(Message);
            }
            catch (MemberServiceException ex) // Catch specific member service exceptions
            {
                return BadRequest($"Registration failed. Please check the provided information. {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("[action]"), AllowAnonymous]
        public IActionResult ActivateRegistration(string memberId, string confirmationCode)
        {
            try
            {
                string Message = _memberService.ActivateRegistration(memberId, confirmationCode);

                return Ok(Message);
            }
            catch (MemberServiceException ex) // Catch specific member service exceptions
            {
                return BadRequest($"Activation failed. Please check the provided information. {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
