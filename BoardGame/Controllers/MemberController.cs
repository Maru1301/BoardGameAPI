using BoardGame.Models.ViewModels;
using BoardGame.Services;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Net;
using System.Security.Claims;

namespace BoardGame.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;

        private readonly JwtHelper _jwt;

        public MemberController(IMemberService memberService, JwtHelper jwt)
        {
            _memberService = memberService;
            _jwt = jwt;
        }

        [HttpGet("[action]"), Authorize(Roles = "admin")]
        public bool Test()
        {
            return true;
        }

        [HttpPost("[action]"), AllowAnonymous]
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

    public class JwtSettingsOptions
    {
        public string Issuer { get; set; } = "";
        public string SignKey { get; set; } = "";
    }

    public class JwtHelper
    {
        private readonly IConfiguration _configuration;

        public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string userName, int expireMinutes = 120)
        {
            //發行人
            var issuer = _configuration.GetValue<string>("JwtSettings:ValidIssuer");
            //加密的key，拿來比對jwt-token沒有
            var signKey = _configuration.GetValue<string>("JwtSettings:Secret");
            //建立JWT - Token
            var token = JwtBuilder.Create()
                      //所採用的雜湊演算法
                      .WithAlgorithm(new HMACSHA256Algorithm()) // symmetric
                                                                //加密key
                      .WithSecret(signKey)
                      //角色
                      .AddClaim("roles", "admin")
                      //JWT ID
                      .AddClaim("jti", Guid.NewGuid().ToString())
                      //發行人
                      .AddClaim("iss", issuer)
                      //使用對象名稱
                      .AddClaim("sub", userName) // User.Identity.Name
                                                 //過期時間
                      .AddClaim("exp", DateTimeOffset.UtcNow.AddMinutes(expireMinutes).ToUnixTimeSeconds())
                      //此時間以前是不可以使用
                      .AddClaim("nbf", DateTimeOffset.UtcNow.ToUnixTimeSeconds())
                      //發行時間
                      .AddClaim("iat", DateTimeOffset.UtcNow.ToUnixTimeSeconds())
                      //使用者全名
                      .AddClaim(ClaimTypes.Name, userName)
                      //進行編碼
                      .Encode();
            return token;
        }
    }
}
