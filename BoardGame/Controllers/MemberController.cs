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
using static BoardGame.Models.ViewModels.MemberVMs;

namespace BoardGame.Controllers
{
    [AuthorizeRoles(Role.Member, Role.Guest, Role.Admin)]
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController(IMemberService memberService, JWTHelper jwt) : ControllerBase
    {
        private readonly IMemberService _memberService = memberService;

        private readonly JWTHelper _jwt = jwt;

        [HttpGet("[action]"), AuthorizeRoles(Role.Admin)]
        public async Task<IActionResult> ListMembers()
        {
            try
            {
                var members = await _memberService.ListMembers();
                return Ok(members.Select(m => m.To<MemberVM>()).ToList());
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

        [HttpGet("[action]")]
        public async Task<IActionResult> GetMemberInfo()
        {
            try
            {
                // Get the current user from the HttpContext
                var user = HttpContext.User;

                // Extract the user ID (or other relevant claim) from the JWT claim
                var idClaim = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier) ?? throw new Exception("Error occured while parsing JWT");
                var id = idClaim.Value;

                if(string.IsNullOrEmpty(id)) return NotFound();

                // Use the user ID to retrieve member information from your database
                var member = await _memberService.GetMemberInfo(new ObjectId(id));

                // Return the member information 
                return Ok(member.To<MemberVM>());
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
                 var (Id, role) = await _memberService.ValidateUser(login.To<LoginDTO>());
                if (Id == ObjectId.Empty || string.IsNullOrEmpty(role))
                {
                    return BadRequest("Invalid Account or Password.");
                }

                // Authorize the user and generate a JWT token.
                var token = _jwt.GenerateToken(Id, login.Account, role);
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
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            try
            {
                // Define a template for the confirmation email URL.
                string confirmationUrlTemplate = "https://localhost:44318/Member/ActivateRegistration";

                string Message = await _memberService.Register(vm.To<RegisterDTO>(), confirmationUrlTemplate);

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

        [HttpPost("[action]")]
        public async Task<string> EditMemberInfo(EditVM vm)
        {
            try
            {
                var user = HttpContext.User;

                string Id = user.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

                var dto = vm.To<EditDTO>();

                dto.Id = new ObjectId(Id);

                string Message = await _memberService.EditMemberInfo(dto);

                return Message;
            }
            catch (MemberServiceException ex) // Catch specific member service exceptions
            {
                return $"Registration failed. Please check the provided information. {ex.Message}";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost("[action]")]
        public async Task<string> ResetPassword(ResetPasswordVM vm)
        {
            try
            {
                var user = HttpContext.User;

                string Id = user.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

                var dto = vm.To<ResetPasswordDTO>();

                dto.Id = new ObjectId(Id);

                string Message = await _memberService.ResetPassword(dto);

                return Message;
            }
            catch (MemberServiceException ex) // Catch specific member service exceptions
            {
                return $"Registration failed. Please check the provided information. {ex.Message}";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpGet("[action]"), AllowAnonymous]
        public async Task<IActionResult> ActivateRegistration(string memberId, string confirmationCode)
        {
            try
            {

                string Message = await _memberService.ActivateRegistration(new ObjectId(memberId), confirmationCode);

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
