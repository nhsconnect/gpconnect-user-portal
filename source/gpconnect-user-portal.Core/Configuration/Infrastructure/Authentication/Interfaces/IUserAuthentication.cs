using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Core.Configuration.Infrastructure.Authentication.Interfaces
{
    internal interface IUserAuthentication
    {
        Task ExecutionTokenValidation(TokenValidatedContext context);
    }
}
