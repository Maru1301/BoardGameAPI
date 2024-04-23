using BoardGame.Models.ViewModels;
using BoardGame.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BoardGame.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet("[action]")]
        public bool Test()
        {
            return true;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginVM login)
        {
            try
            {
                 var result = await _memberService.ValidateUser(login.ToDTO());
                if (result == false)
                {
                    return BadRequest("Invalid username or password.");
                }

                // Authorize the user and generate a JWT token.
                var token = _memberService.GenerateToken(login.Account);
                return Ok(token);
            }
            catch(MemberServiceException ex)
            {
                return BadRequest($"Registration failed. Please check the provided information. {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            try
            {
                // Define a template for the confirmation email URL.
                string confirmationUrlTemplate = "https://localhost:44318/Member/ActivateRegistration";

                string Message = await _memberService.Register(register.ToDTO(), confirmationUrlTemplate);

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

        [HttpGet("[action]")]
        [AllowAnonymous]
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
