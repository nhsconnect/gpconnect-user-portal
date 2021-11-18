using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace gpconnect_user_portal.Models.Interfaces
{
    public interface ICommon
    {
        public IHttpContextAccessor _httpContextAccessor { get; set; }
        public ILogService _logService { get; set; }
    }
}
