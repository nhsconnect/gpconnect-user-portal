﻿using gpconnect_user_portal.Helpers;
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

            var userId = context.Principal.GetClaimValue("UserId");
            var userSessionId = context.Principal.GetClaimValue("UserSessionId");

            if (userId == null)
            {
                context.RejectPrincipal();
            }

            NLog.LogManager.Configuration.Variables["userId"] = userId;
            NLog.LogManager.Configuration.Variables["userSessionId"] = userSessionId;

            context.Options.ExpireTimeSpan = TimeSpan.FromMinutes(30);

            return Task.CompletedTask;
        }
    }
}
