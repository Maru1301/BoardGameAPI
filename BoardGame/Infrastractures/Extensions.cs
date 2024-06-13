using AutoMapper;
using System.Security.Claims;

namespace BoardGame.Infrastractures
{
    public static class Extensions
    {
        /// <summary>
        /// Attempts to convert an object of any type to a specified target type <typeparamref name="TResult"/> using AutoMapper.
        /// </summary>
        /// <typeparam name="TResult">The target type to which the object should be converted.</typeparam>
        /// <param name="source">The object to be converted.</param>
        /// <returns>An instance of the target type <typeparamref name="TResult"/> populated with the mapped values from the source object, 
        /// or null if the conversion fails.</returns>
        public static TResult To<TResult>(this object source) where TResult : new()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap(source.GetType(), typeof(TResult));
            });
            IMapper mapper = config.CreateMapper();
            var target = mapper.Map<TResult>(source);
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
