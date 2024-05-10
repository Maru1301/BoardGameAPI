using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BoardGame.Infrastractures
{
    public class JWTHelper(IOptionsMonitor<JwtSettingsOptions> settings)
    {
        private readonly JwtSettingsOptions _settings = settings.CurrentValue;

        public string GenerateToken(ObjectId Id, string account, string role, int expireHours = 1)
        {
            var issuer = _settings.ValidIssuer;
            var signKey = _settings.Secret;

            var token = BuildToken(Id, account, role, expireHours, issuer, signKey);

            return token;
        }

        private static string BuildToken(ObjectId Id, string account, string role, int expireHours, string issuer, string secret)
        {
            List<Claim> claims =
            [
                new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                new Claim(ClaimTypes.Name, account),
                new Claim(ClaimTypes.Role, role),
            ];

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secret));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken
            (
                claims: claims,
                expires: DateTime.UtcNow.AddHours(expireHours),
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
