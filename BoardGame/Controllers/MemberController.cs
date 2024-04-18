using BoardGame.Models.EFModels;
using BoardGame.Models.ViewModels;
using BoardGame.Services;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Register(RegisterVM register)
        {
            string confirmationUrlTemplate = "http://localhost:8080/Member/MemberActivate";

            (bool Success,string Message) = _memberService.Register(register.ToMemberDTO(), confirmationUrlTemplate);
            if (!Success)
            {
                return BadRequest(Message);
            }

            return Ok(Message);
        }
    }
}
