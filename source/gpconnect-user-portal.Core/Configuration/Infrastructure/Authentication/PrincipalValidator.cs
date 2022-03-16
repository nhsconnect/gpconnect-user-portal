using gpconnect_user_portal.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Core.Configuration.Infrastructure.Authentication
{
    internal static class PrincipalValidator
    {
        internal static Task ValidateAsync(CookieValidatePrincipalContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            context.Options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            return Task.CompletedTask;
        }

        internal static Task RedirectAsync(RedirectContext<CookieAuthenticationOptions> context)
        {
            context.Response.Redirect("/");
            return Task.CompletedTask;
        }
    }
}
