using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Core.Configuration.Infrastructure.Authentication
{
    public class AuthorisedUserHandler : AuthorizationHandler<AuthorisedUserRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorisedUserRequirement requirement)
        {
            var authFilterContext = context.Resource as DefaultHttpContext;
            if (!context.User.Identity.IsAuthenticated)
            {
                authFilterContext.Response.Redirect("/Index");
                context.Fail();
            }
            else
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
