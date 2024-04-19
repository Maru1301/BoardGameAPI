using BoardGame.Models.ViewModels;
using BoardGame.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BoardGame.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public IActionResult Register(RegisterVM register)
        {
            try
            {
                // Define a template for the confirmation email URL.
                string confirmationUrlTemplate = "https://localhost:44318/Member/ActivateRegistration";

                string Message = _memberService.Register(register.ToMemberDTO(), confirmationUrlTemplate);

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
