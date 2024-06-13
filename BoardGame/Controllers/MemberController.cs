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
    public class MemberController(IMemberService memberService, ILogger<MemberController> logger) : ControllerBase
    {
        private readonly IMemberService _memberService = memberService;
        private readonly ILogger<MemberController> _logger = logger;

        [HttpGet, AuthorizeRoles(Role.Admin)]
        public async Task<IActionResult> ListMembers()
        {
            try
            {
                //_logger.LogInformation("ListMember");
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

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] MemberLoginRequestDTO login)
        {
            //_logger.LogInformation($"{login.Account} tries to login");
            try
            {
                var token = await _memberService.ValidateUser(login.To<LoginDTO>());

                return Ok(new { token });
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

                string message = await _memberService.Register(vm.To<RegisterDTO>(), domainName);

                return Ok(new { message });
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

        [HttpGet]
        public async Task<IActionResult> ResendConfirmationCode()
        {
            try
            {
                string memberId = HttpContext.GetJwtClaim(ClaimTypes.NameIdentifier).Value;
                var domainName = GetDomainName();

                string message = await _memberService.ResendConfirmationCode(new ObjectId(memberId), domainName);
                
                return Ok(new { message });
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
        public async Task<IActionResult> EditMemberInfo(EditRequestDTO vm)
        {
            try
            {
                string memberId = HttpContext.GetJwtClaim(ClaimTypes.NameIdentifier).Value;

                var dto = vm.To<EditDTO>();

                dto.Id = new ObjectId(memberId);

                string message = await _memberService.EditMemberInfo(dto);

                return Ok(new { message });
            }
            catch (MemberServiceException ex) // Catch specific member service exceptions
            {
                return BadRequest($"Edition failed. Please check the provided information. {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequestDTO vm)
        {
            try
            {
                string memberId = HttpContext.GetJwtClaim(ClaimTypes.NameIdentifier).Value;

                var dto = vm.To<ResetPasswordDTO>();

                dto.Id = new ObjectId(memberId);

                string message = await _memberService.ResetPassword(dto);

                return Ok(new { message });
            }
            catch (MemberServiceException ex) // Catch specific member service exceptions
            {
                return BadRequest($"Reset failed. Please check the provided information. {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> ValidateEmail(string memberId, string confirmationCode)
        {
            try
            {
                string message = await _memberService.ValidateEmail(memberId, confirmationCode);

                return Ok(new { message });
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

        [HttpDelete, AllowAnonymous]
        public async Task<IActionResult> Delete(string account)
        {
            try
            {
                var role = HttpContext.GetJwtClaim(ClaimTypes.Role).Value;
                var jwtAccount = HttpContext.GetJwtClaim(ClaimTypes.Name).Value;

                if (role != Role.Admin && account != jwtAccount)
                {
                    return BadRequest(ErrorCode.AccountNotMatch);
                }

                var result = await _memberService.Delete(account);

                return Ok(new { result });
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
    }
}
