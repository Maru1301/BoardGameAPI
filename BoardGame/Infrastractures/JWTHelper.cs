﻿using FluentResults;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BoardGame.Infrastractures
{
    public class JWTHelper(IConfig config)
    {
        public Result<string> GenerateToken(ObjectId Id, string account, Role role)
            => BuildToken(Id, account, role);

        private Result<string> BuildToken(ObjectId Id, string account, Role role)
        {
            List<Claim> claims =
            [
                new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                new Claim(ClaimTypes.Name, account),
                new Claim(ClaimTypes.Role, role.ToString()),
            ];

            if (string.IsNullOrEmpty(config.JwtConfig.IssuerSigningKey))
            {
                return Result.Fail("Issuer signing key is missing!");
            }

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(config.JwtConfig.IssuerSigningKey));
            var cred = new SigningCredentials(key, config.JwtConfig.Algorithm);
            _ = double.TryParse(config.JwtConfig.ExpiredTime, out double expiredTime);
            var token = new JwtSecurityToken
            (
                issuer: config.JwtConfig.ValidIssuer,
                audience: config.JwtConfig.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddSeconds(expiredTime),
                signingCredentials: cred
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return Result.Ok(jwt);
        }
    }

    /// <summary>
    /// Jwt settings
    /// </summary>
    public sealed class JWTSettingsOptions
    {
        /// <summary>
        /// Whether to validate the issuer signing key.
        /// </summary>
        public bool? ValidateIssuerSigningKey { get; set; }

        /// <summary>
        /// The issuer signing key.
        /// </summary>
        public string IssuerSigningKey { get; set; } = string.Empty;

        /// <summary>
        /// Whether to validate the issuer.
        /// </summary>
        public bool? ValidateIssuer { get; set; }

        /// <summary>
        /// The valid issuer.
        /// </summary>
        public string ValidIssuer { get; set; } = string.Empty;

        /// <summary>
        /// Whether to validate the audience.
        /// </summary>
        public bool? ValidateAudience { get; set; }

        /// <summary>
        /// The valid audience.
        /// </summary>
        public string ValidAudience { get; set; } = string.Empty;

        /// <summary>
        ///  Whether to validate the lifetime.
        /// </summary>
        public bool? ValidateLifetime { get; set; }

        /// <summary>
        /// The clock skew value (in seconds) to tolerate time discrepancies between servers.
        /// </summary>
        public long? ClockSkew { get; set; }

        /// <summary>
        /// The expiration time (in minutes).
        /// </summary>
        public long? ExpiredTime { get; set; }

        /// <summary>
        /// The encryption algorithm.
        /// </summary>
        public string Algorithm { get; set; } = string.Empty;

        /// <summary>
        /// Whether to validate the expiration time. Set to false for no expiration. The default value is true.
        /// </summary>
        public bool ExpirationEnabled { get; set; } = true;
    }
}
