using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BoardGame.Infrastractures
{
    public static class Extensions
    {
        /// <summary>
        /// Retrieves a specific claim from the current user's JWT token.
        /// </summary>
        /// <param name="httpContext">The current HttpContext instance.</param>
        /// <param name="claimType">The type of claim to retrieve (e.g., "name", "email").</param>
        /// <returns>The claim object containing the requested claim value, or null if the claim is not found.</returns>
        /// <exception cref="Exception">Throws an exception if there is an error parsing the JWT token (uses ErrorCode.ErrorParsingJwt).</exception>
        public static Claim GetJwtClaim(this HttpContext httpContext, string claimType)
        {
            return httpContext.User.Claims.FirstOrDefault(x => x.Type == claimType) ?? 
                throw new Exception(ErrorCode.ErrorParsingJwt);
        }

        public static OkObjectResult Ok(this string s)
        {
            return new OkObjectResult(new { s });
        }

        public static OkObjectResult Ok(this bool result)
        {
            return new OkObjectResult(new { result });
        }
    }
}
