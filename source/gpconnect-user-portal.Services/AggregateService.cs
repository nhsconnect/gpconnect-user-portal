using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace gpconnect_user_portal.Services
{
    public class AggregateService : IAggregateService
    {
        public AggregateService(IHttpContextAccessor httpContextAccessor, ILogService logService)
        {
            LogService = logService;
            HttpContextAccessor = httpContextAccessor;
        }

        public ILogService LogService { get; set; }
        public IHttpContextAccessor HttpContextAccessor { get; }
    }
}
