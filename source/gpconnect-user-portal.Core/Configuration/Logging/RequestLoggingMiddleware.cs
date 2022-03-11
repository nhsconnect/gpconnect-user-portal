using gpconnect_user_portal.DTO.Request.Logging;
using gpconnect_user_portal.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Core.Configuration.Logging
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestLoggingMiddleware>();
        }

        public async Task Invoke(HttpContext context)
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
                    var webRequest = new WebRequest
                    {
                        Url = url,
                        Description = "",
                        Ip = context.Connection?.LocalIpAddress.ToString(),
                        Server = context.Request?.Host.Host,
                        SessionId = context.GetSessionId(),
                        ReferrerUrl = context.Request?.Headers["Referer"].ToString(),
                        ResponseCode = context.Response.StatusCode,
                        UserAgent = context.Request?.Headers["User-Agent"].ToString()
                    };
                    _logger.LogInformation(webRequest.ConvertObjectToJsonData());
                }
            }
        }
    }
}
