using gpconnect_user_portal.Framework.Configuration.Infrastructure.Logging.Interfaces;
using NLog;

namespace gpconnect_user_portal.Framework.Configuration.Infrastructure.Logging
{
    public class LoggerManager : ILoggerManager
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public void LogFatal(string message)
        {
            logger.Fatal(message);
        }
        public void LogDebug(string message)
        {
            logger.Debug(message);
        }
        public void LogError(string message)
        {
            logger.Error(message);
        }
        public void LogInformation(string message)
        {
            logger.Info(message);
        }
        public void LogWarning(string message)
        {
            logger.Warn(message);
        }
    }
}
