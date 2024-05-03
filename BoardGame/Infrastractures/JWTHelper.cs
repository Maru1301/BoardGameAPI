using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BoardGame.Infrastractures
{
    public class JWTHelper
    {
        private readonly JwtSettingsOptions _settings;

        public JWTHelper(IOptionsMonitor<JwtSettingsOptions> settings)
        {
            _settings = settings.CurrentValue;
        }

        public string GenerateToken(string account, string role, int expireMinutes = 120)
        {
            var issuer = _settings.ValidIssuer;
            var signKey = _settings.Secret;

            var token = BuildToken(account, role, expireMinutes, issuer, signKey);

            return token;
        }

        private static string BuildToken(string account, string role, int expireMinutes, string issuer, string secret)
        {
            List<Claim> claims =
            [
                new Claim(ClaimTypes.Name, account),
                new Claim(ClaimTypes.Role, role),
            ];

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secret));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken
            (
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: cred
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }

    public class JwtSettingsOptions
    {
        public string ValidIssuer { get; set; } = string.Empty;
        public string Secret { get; set; } = string.Empty;
    }
}
