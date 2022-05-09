using gpconnect_user_portal.Core.Configuration.Infrastructure.Logging.Interfaces;
using NLog;

namespace gpconnect_user_portal.Core.Configuration.Logging
{
    public class WebLoggerManager : IWebLoggerManager
    {
        private static readonly ILogger _webLogger = LogManager.GetLogger("Splunk_WebIndex");

        public void LogWebRequest(string webRequest)
        {
            //_webLogger.Trace(webRequest);
        }
    }
}
