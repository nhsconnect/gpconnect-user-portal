using gpconnect_user_portal.Core.Configuration.Infrastructure.Logging.Interfaces;
using gpconnect_user_portal.DTO.Request.Logging;
using gpconnect_user_portal.Helpers;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Core.Configuration.Logging
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebLoggerManager _logger;

        public RequestLoggingMiddleware(RequestDelegate next, IWebLoggerManager logger)
        {
            _next = next;
            _logger = logger;
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
                        Method = context.Request?.Method,
                        Ip = context.Connection?.LocalIpAddress.ToString(),
                        Host = context.Request?.Host,
                        SessionId = context.GetSessionId(),
                        ReferrerUrl = context.Request?.Headers["Referer"],
                        StatusCode = context.Response.StatusCode,
                        UserAgent = context.Request?.Headers["User-Agent"]
                    };
                    _logger.LogWebRequest(webRequest.ConvertObjectToJsonData());

                    if (context.Request.HasFormContentType && context.Request.Form != null && context.Request.Form.Count() > 0)
                    {
                        _logger.LogWebRequest(context.Request.Form.ConvertObjectToJsonData());
                    }

                    if (context.Response.Headers != null && context.Response.Headers.Count() > 0)
                    {
                        _logger.LogWebRequest(context.Response.Headers.ConvertObjectToJsonData());
                    }
                };                
            }
        }
    }
}