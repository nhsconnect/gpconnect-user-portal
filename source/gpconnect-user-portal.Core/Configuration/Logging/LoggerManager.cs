using gpconnect_user_portal.Core.Configuration.Infrastructure.Logging.Interfaces;
using NLog;

namespace gpconnect_user_portal.Core.Configuration.Logging
{
    public class LoggerManager : ILoggerManager
    {
        private static ILogger _errorLogger = LogManager.GetLogger("Splunk_ErrorIndex");
        private static ILogger _logLogger = LogManager.GetLogger("Splunk_LogsIndex");
                
        public void LogFatal(string message)
        {
            _errorLogger.Fatal(message);
        }
        public void LogDebug(string message)
        {
            _logLogger.Debug(message);
        }
        public void LogError(string message)
        {
            _errorLogger.Error(message);
        }
        public void LogInformation(string message)
        {
            _logLogger.Info(message);
        }
        public void LogWarning(string message)
        {
            _errorLogger.Warn(message);
        }
    }
}
