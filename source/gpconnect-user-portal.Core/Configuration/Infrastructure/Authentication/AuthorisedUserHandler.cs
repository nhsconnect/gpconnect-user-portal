using gpconnect_user_portal.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Core.Configuration.Infrastructure.Authentication
{
    public class AuthorisedUserHandler : AuthorizationHandler<AuthorisedUserRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorisedUserRequirement requirement)
        {
            if (context.User.Identity.IsAuthenticated && context.User.GetClaimValue("IsAdmin").StringToBoolean())
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
