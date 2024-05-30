using BoardGame.Authorizations;
using BoardGame.Infrastractures;
using BoardGame.Models.DTOs;
using BoardGame.Services;
using BoardGame.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Net;
using System.Security.Claims;

namespace BoardGame.Controllers
{
    [AuthorizeRoles(Role.Member, Role.Guest, Role.Admin)]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MemberController(IMemberService memberService) : ControllerBase
    {
        private readonly IMemberService _memberService = memberService;

        [HttpGet, AuthorizeRoles(Role.Admin)]
        public async Task<IActionResult> ListMembers()
        {
            try
            {
                var members = await _memberService.ListMembers();
                return Ok(members.Select(m => m.To<MemberResponseDTO>()).ToList());
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

        [HttpGet]
        public async Task<IActionResult> GetMemberInfo()
        {
            try
            {
                // Extract the user ID (or other relevant claim) from the JWT claim
                var id = HttpContext.GetJwtClaim(ClaimTypes.NameIdentifier).Value;

                if(string.IsNullOrEmpty(id)) return NotFound();

                var member = await _memberService.GetMemberInfo(new ObjectId(id));

                return Ok(member.To<MemberResponseDTO>());
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

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> Login([FromQuery] MemberLoginRequestDTO login)
        {
            try
            {
                var token = await _memberService.ValidateUser(login.To<LoginDTO>());

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
        
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(RegisterRequestDTO vm)
        {
            try
            {
                var domainName = GetDomainName();

                string Message = await _memberService.Register(vm.To<RegisterDTO>(), domainName);

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

        private string GetDomainName()
        {
            return $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
        }

        [HttpGet]
        public async Task<IActionResult> ResendConfirmationCode()
        {
            try
            {
                string memberId = HttpContext.GetJwtClaim(ClaimTypes.NameIdentifier).Value;
                var domainName = GetDomainName();

                string message = await _memberService.ResendConfirmationCode(new ObjectId(memberId), domainName);
                
                return Ok(message);
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

        [HttpPost]
        public async Task<string> EditMemberInfo(EditRequestDTO vm)
        {
            try
            {
                string memberId = HttpContext.GetJwtClaim(ClaimTypes.NameIdentifier).Value;

                var dto = vm.To<EditDTO>();

                dto.Id = new ObjectId(memberId);

                string Message = await _memberService.EditMemberInfo(dto);

                return Message;
            }
            catch (MemberServiceException ex) // Catch specific member service exceptions
            {
                return $"Edition failed. Please check the provided information. {ex.Message}";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost]
        public async Task<string> ResetPassword(ResetPasswordRequestDTO vm)
        {
            try
            {
                string memberId = HttpContext.GetJwtClaim(ClaimTypes.NameIdentifier).Value;

                var dto = vm.To<ResetPasswordDTO>();

                dto.Id = new ObjectId(memberId);

                string Message = await _memberService.ResetPassword(dto);

                return Message;
            }
            catch (MemberServiceException ex) // Catch specific member service exceptions
            {
                return $"Reset failed. Please check the provided information. {ex.Message}";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> ValidateEmail(string memberId, string confirmationCode)
        {
            try
            {
                string Message = await _memberService.ValidateEmail(new ObjectId(memberId), confirmationCode);

                return Ok(Message);
            }
            catch (MemberServiceException ex) // Catch specific member service exceptions
            {
                return BadRequest($"Validation failed. Please check the provided information. {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
