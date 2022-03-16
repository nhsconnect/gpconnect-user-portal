namespace gpconnect_user_portal.Core.Configuration.Infrastructure.Logging.Interfaces
{
    public interface ILoggerManager
    {
        void LogFatal(string message);
        void LogInformation(string message);
        void LogWarning(string message);
        void LogDebug(string message);
        void LogError(string message);
    }
}
