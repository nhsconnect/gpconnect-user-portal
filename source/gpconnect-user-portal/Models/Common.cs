using gpconnect_user_portal.Models.Interfaces;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace gpconnect_user_portal.Models
{
    public class Common : ICommon
    {
        public Common(IHttpContextAccessor httpContextAccessor, ILogService logService)
        {
            _httpContextAccessor = httpContextAccessor;
            _logService = logService;
        }

        public IHttpContextAccessor _httpContextAccessor { get; set; }
        public ILogService _logService { get; set; }
    }
}
