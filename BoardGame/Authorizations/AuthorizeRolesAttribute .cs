using BoardGame.Infrastractures;
using Microsoft.AspNetCore.Authorization;

namespace BoardGame.Authorizations
{
    internal class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params Role[] roles)
        {
            Roles = string.Join(",", roles);
        }
    }
}
