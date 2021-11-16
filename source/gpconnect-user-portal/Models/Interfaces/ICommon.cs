using Microsoft.AspNetCore.Http;

namespace gpconnect_user_portal.Models.Interfaces
{
    public interface ICommon
    {
        IHttpContextAccessor httpContextAccessor { get; }
    }
}
