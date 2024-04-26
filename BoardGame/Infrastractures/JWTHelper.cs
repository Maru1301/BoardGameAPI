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

        public string GenerateToken(string account, string role, int expireMinutes = 120)
        {
            var issuer = _settings.ValidIssuer;
            var signKey = _settings.Secret;

            var token = BuildToken(account, role, expireMinutes, issuer, signKey);

            return token;
        }

        private static string BuildToken(string account, string role, int expireMinutes, string issuer, string signKey)
        {
            var jwtBuilder = JwtBuilder.Create()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(signKey)
                .AddClaim("Roles", role)
                .AddClaim("jti", Guid.NewGuid().ToString())
                .AddClaim("iss", issuer)
                .AddClaim("sub", account)
                .AddClaim("exp", GetExpirationTime(expireMinutes))
                .AddClaim("nbf", GetCurrentTime())
                .AddClaim("iat", GetCurrentTime())
                .AddClaim(ClaimTypes.Name, account);

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
