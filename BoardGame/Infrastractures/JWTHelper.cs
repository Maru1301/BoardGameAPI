using JWT.Algorithms;
using JWT.Builder;
using Microsoft.Extensions.Options;
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

        public string GenerateToken(string userName, string role, int expireMinutes = 120)
        {
            var issuer = _settings.ValidIssuer;
            var signKey = _settings.Secret;

            var token = BuildToken(userName, role, expireMinutes, issuer, signKey);

            return token;
        }

        private static string BuildToken(string userName,string role, int expireMinutes, string issuer, string signKey)
        {
            var jwtBuilder = JwtBuilder.Create()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(signKey)
                .AddClaim("roles", role)
                .AddClaim("jti", Guid.NewGuid().ToString())
                .AddClaim("iss", issuer)
                .AddClaim("sub", userName)
                .AddClaim("exp", GetExpirationTime(expireMinutes))
                .AddClaim("nbf", GetCurrentTime())
                .AddClaim("iat", GetCurrentTime())
                .AddClaim(ClaimTypes.Name, userName);

            return jwtBuilder.Encode();
        }

        private static long GetExpirationTime(int expireMinutes)
        {
            return DateTimeOffset.UtcNow.AddMinutes(expireMinutes).ToUnixTimeSeconds();
        }

        private static long GetCurrentTime()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

    }

    public class JwtSettingsOptions
    {
        public string ValidIssuer { get; set; } = string.Empty;
        public string Secret { get; set; } = string.Empty;
    }
}
