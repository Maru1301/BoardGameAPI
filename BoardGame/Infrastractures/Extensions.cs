using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Transactions;

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

        public static async Task<List<T>?> ToListWithNoLockAsync<T>(
            this IQueryable<T> query,
            Expression<Func<T, bool>>? expression = null,
            CancellationToken cancellationToken = default)
        {
            using var scope = CreateTrancation();

            if (expression != null)
            {
                query = query.Where(expression);
            }

            var result = await query.ToListAsync(cancellationToken);

            scope.Complete();

            return result;
        }

        private static TransactionScope CreateTrancation()
        {
            return new TransactionScope(TransactionScopeOption.Required,
                                        new TransactionOptions()
                                        {
                                            IsolationLevel = IsolationLevel.ReadUncommitted
                                        },
                                       TransactionScopeAsyncFlowOption.Enabled);
        }
    }
}
