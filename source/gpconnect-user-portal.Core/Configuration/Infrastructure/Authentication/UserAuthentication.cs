using gpconnect_user_portal.Core.Configuration.Infrastructure.Authentication.Interfaces;
using gpconnect_user_portal.DTO.Request.Application;
using gpconnect_user_portal.Helpers;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Core.Configuration.Infrastructure.Authentication
{
    public class UserAuthentication : IUserAuthentication
    {
        private readonly IApplicationService _applicationService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILogger<UserAuthentication> _logger;

        public UserAuthentication(IApplicationService applicationService, IHttpContextAccessor contextAccessor, ILogger<UserAuthentication> logger)
        {
            _applicationService = applicationService;
            _contextAccessor = contextAccessor;
            _logger = logger;
        }

        public Task ExecutionTokenValidation(TokenValidatedContext context)
        {
            try
            {
                if (context == null) throw new ArgumentNullException(nameof(context));
                if (context.Principal == null) throw new ArgumentNullException(nameof(context.Principal));
                return PerformRedirectionBasedOnStatus(context);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "An error occurred attempting to authorise the user");
                throw;
            }
        }

        private async Task PerformRedirectionBasedOnStatus(TokenValidatedContext context)
        {
            var emailAddress = StringExtensions.Coalesce(context.Principal.GetClaimValue("Email", nullIfEmpty: true), context.Principal.GetClaimValue("Email Address", nullIfEmpty: true));
            if (emailAddress != null)
            {
                var loggedOnUser = await LogonUser(emailAddress, context);
                PopulateAdditionalClaims(loggedOnUser, emailAddress, context);
                context.Properties.RedirectUri = "/";
            }            
        }

        private async Task<DTO.Response.Application.User> LogonUser(string emailAddress, TokenValidatedContext context)
        {
            var loggedOnUser = await _applicationService.LogonUser(new User
            {
                EmailAddress = emailAddress
            });
            return loggedOnUser;
        }

        private void PopulateAdditionalClaims(DTO.Response.Application.User loggedOnUser, string emailAddress, TokenValidatedContext context)
        {
            if (context.Principal.Identity is ClaimsIdentity identity)
            {
                identity.AddOrReplaceClaimValue("Email", emailAddress);
                
                if (loggedOnUser != null)
                {
                    identity.AddClaim(new Claim("UserSessionId", loggedOnUser.UserSessionId.ToString()));
                    identity.AddClaim(new Claim("UserId", loggedOnUser.UserId.ToString()));
                    identity.AddClaim(new Claim("IsAdmin", loggedOnUser.IsAdmin.ToString()));
                    identity.AddClaim(new Claim("LastLogonDate", loggedOnUser.LastLogonDate.ToString()));
                }
            }
        }
    }
}