using AutoMapper;
using System.Security.Claims;

namespace BoardGame.Infrastractures
{
    public static class Extensions
    {
        public static T To<T>(this object source) where T : new()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap(source.GetType(), typeof(T));
            });
            IMapper mapper = config.CreateMapper();
            var target = mapper.Map<T>(source);
            return target;
        }

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
    }
}
