using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;

namespace IMDb.Api.Extensions
{
    public class CustomAuthorize
    {
        public static bool validateUserClaims(HttpContext context, string claimName, string claimValue)
        {
            var isAuthenticated = context.User.Identity.IsAuthenticated;
            var haveClaims = context.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));

            return isAuthenticated & haveClaims;
        }
    }

    public class AuthorizeToClaimValue : TypeFilterAttribute
    {
        public AuthorizeToClaimValue(string claimName, string claimValue) : base(typeof(ClaimFilter))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }

    public class ClaimFilter : IAuthorizationFilter
    {
        private readonly Claim _claim;

        public ClaimFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new StatusCodeResult(401);
                return;
            }

            if (!CustomAuthorize.validateUserClaims(context.HttpContext, _claim.Type, _claim.Value))
            {
                context.Result = new StatusCodeResult(403);
            }
        }
    }
}
