using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace gpconnect_user_portal.Services
{
    public class AggregateService : IAggregateService
    {
        public AggregateService(IHttpContextAccessor httpContextAccessor, ILogService logService, IApplicationService applicationService, IReferenceService referenceService, IExportService exportService, IQueryService queryService, ICoreService coreService, IEmailService emailService)
        {
            LogService = logService;
            HttpContextAccessor = httpContextAccessor;
            ApplicationService = applicationService;
            CoreService = coreService;
            ReferenceService = referenceService;
            ExportService = exportService;
            QueryService = queryService;
            EmailService = emailService;
        }

        public ILogService LogService { get; }
        public IApplicationService ApplicationService { get; }
        public ICoreService CoreService { get; }
        public IReferenceService ReferenceService { get; }
        public IExportService ExportService { get; }
        public IQueryService QueryService { get; }
        public IEmailService EmailService { get; }
        public IHttpContextAccessor HttpContextAccessor { get; }

        public HttpRequest Request => HttpContextAccessor.HttpContext.Request;
        public HttpResponse Response => HttpContextAccessor.HttpContext.Response;
        public ClaimsPrincipal User => HttpContextAccessor.HttpContext.User;
        public ISession Session => HttpContextAccessor.HttpContext.Session;
    }
}
