using BoardGame.Infrastractures;
using BoardGame.Models.DTOs;
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

        private readonly JWTHelper _jwt;

        public MemberController(IMemberService memberService, JWTHelper jwt)
        {
            _memberService = memberService;
            _jwt = jwt;
        }

        [HttpGet("[action]"), Authorize(Roles = "admin")]
        public IActionResult ListMembers()
        {
            try
            {
                var members = _memberService.ListMembers();
                return Ok(members.Select(m => m.ToVM<MemberVM>()).ToList());
            }
            catch (MemberServiceException ex)
            {
                return BadRequest($"Registration failed. Please check the provided information. {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost("[action]"), AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginVM login)
        {
            try
            {
                 var result = await _memberService.ValidateUser(login.ToDTO<LoginDTO>());
                if (result == false)
                {
                    return BadRequest("Invalid username or password.");
                }

                // Authorize the user and generate a JWT token.
                var token = _jwt.GenerateToken(login.Account);
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
