using BoardGame.ApiControllers.Models;
using BoardGame.ApiCores;
using BoardGame.Authorizations;
using BoardGame.Infrastractures;
using FluentResults.Extensions.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace BoardGame.ApiControllers
{
    [AuthorizeRoles(Role.Member, Role.Guest, Role.Admin)]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MemberApiController(MemberApiCore memberApiCore) : ControllerBase
    {
        private readonly MemberApiCore core = memberApiCore;
        //private readonly ILogger<MemberApiController> _logger;

        [HttpGet, AuthorizeRoles(Role.Admin)]
        public async Task<IActionResult> ListMembers()
            => await core.ListMembers().ToActionResult();

        [HttpGet, AuthorizeRoles(Role.Member, Role.Guest)]
        public async Task<IActionResult> GetMemberInfo()
        {
            // Extract the user ID (or other relevant claim) from the JWT claim
            var id = User.GetJwtClaim(ClaimTypes.NameIdentifier).Value;
            return await core.GetMemberInfo(id).ToActionResult();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] MemberLoginRequest req)
            => await core.Login(req).ToActionResult();

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(RegisterRequest req)
            => await core.Register(req).ToActionResult();

        [HttpGet]
        public async Task<IActionResult> ResendConfirmationCode()
            => await core.ResendConfirmationCode(GetMemberId()).ToActionResult();

        [HttpPost]
        public async Task<IActionResult> EditMemberInfo(EditMemberInfoRequest req)
        {
            req.MemberId = GetMemberId();
            return await core.EditMemberInfo(req).ToActionResult();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest req)
        {
            req.MemberId = GetMemberId();
            return await core.ResetPassword(req).ToActionResult();
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> ValidateEmail(string memberId, string confirmationCode)
            => await core.ValidateEmail(memberId, confirmationCode).ToActionResult();

        [HttpDelete]
        public async Task<IActionResult> DeleteMember(string? account)
        {
            var req = new DeleteMemberRequest
            {
                InputAccount = account,
                JwtAccount = User.GetJwtClaim(ClaimTypes.Name).Value, // Todo: should judge whether the role is admin or not
                Role = User.GetJwtClaim(ClaimTypes.Role).Value
            };

            return await core.DeleteMember(req).ToActionResult();
        }

        private string GetMemberId()
            => User.GetJwtClaim(ClaimTypes.NameIdentifier).Value;
    }
}
