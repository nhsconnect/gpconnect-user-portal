using gpconnect_user_portal.DTO.Request.Logging;
using gpconnect_user_portal.Helpers;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Core.Configuration.Infrastructure.Logging
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILogService logService)
        {
            try
            {
                await _next(context);
            }
            finally
            {
                var url = context.Request?.Path.Value;
                if (!url.Contains(Helpers.Constants.SystemConstants.HEALTHCHECKERPATH))
                {
                    logService.AddWebRequestLog(new WebRequest
                    {
                        Url = url,
                        Description = "",
                        Ip = context.Connection?.LocalIpAddress.ToString(),
                        Server = context.Request?.Host.Host,
                        SessionId = context.GetSessionId(),
                        ReferrerUrl = context.Request?.Headers["Referer"].ToString(),
                        ResponseCode = context.Response.StatusCode,
                        UserAgent = context.Request?.Headers["User-Agent"].ToString()
                    });
                }
            }
        }
    }

}
