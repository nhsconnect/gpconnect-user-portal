using Microsoft.AspNetCore.Http;

namespace gpconnect_user_portal.Services.Interfaces
{
    public interface IAggregateService
    {
        public ILogService LogService { get; set; }
        public IHttpContextAccessor HttpContextAccessor { get; }
    }
}
