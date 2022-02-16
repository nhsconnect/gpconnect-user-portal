using Microsoft.AspNetCore.Authorization;

namespace gpconnect_user_portal.Core.Configuration.Infrastructure.Authentication
{
    public class AuthorisedUserRequirement : IAuthorizationRequirement
    {
        public AuthorisedUserRequirement()
        {            
        }
    }
}
