using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace gpconnect_user_portal.Services.Interfaces
{
    public interface IAggregateService
    {
        public ILogService LogService { get; }
        public IApplicationService ApplicationService { get; }
        public ICoreService CoreService { get; }
        public IReferenceService ReferenceService { get; }
        public IQueryService QueryService { get; }
        public IExportService ExportService { get; }
        public IHttpContextAccessor HttpContextAccessor { get; }
        public HttpRequest Request { get; }
        public HttpResponse Response { get; }
        public ClaimsPrincipal User { get; }
        public ISession Session { get; }
    }
}
