using BoardGame.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace BoardGame.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly MemberService _memberService;

        public MemberController(MemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpPost("[action]")]
        public IActionResult Register()
        {
            var result = _memberService.Register(member.ToMemberDTO(), urlTemplate);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }
    }
}
