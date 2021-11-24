using gpconnect_user_portal.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Targets;

namespace gpconnect_user_portal.Core.Configuration.Infrastructure.Logging
{
    public static class LoggingExtensions
    {
        public static IServiceCollection ConfigureLoggingServices(this IServiceCollection services, IConfiguration configuration)
        {
            var nLogConfiguration = new NLog.Config.LoggingConfiguration();

            var consoleTarget = AddConsoleTarget();
            var networkTarget = AddNetworkTarget(configuration);

            nLogConfiguration.AddRule(NLog.LogLevel.Trace, NLog.LogLevel.Fatal, consoleTarget);
            nLogConfiguration.AddRule(NLog.LogLevel.Trace, NLog.LogLevel.Fatal, networkTarget);

            nLogConfiguration.AddTarget(consoleTarget);
            nLogConfiguration.AddTarget(networkTarget);

            nLogConfiguration.Variables.Add("applicationVersion", ApplicationHelper.ApplicationVersion.GetAssemblyVersion());

            LogManager.Configuration = nLogConfiguration;

            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddConfiguration(configuration.GetSection("Logging"));
            });

            return services;
        }

        private static NetworkTarget AddNetworkTarget(IConfiguration configuration)
        {
            var networkTarget = new NetworkTarget()
            {
                Name = "Splunk Cloud",
                SslProtocols = System.Security.Authentication.SslProtocols.Tls12
            };
            return networkTarget;
        }

        private static ConsoleTarget AddConsoleTarget()
        {
            var consoleTarget = new ConsoleTarget
            {
                Name = "Console",
                Layout = "${var:applicationVersion}|${date}|${level:uppercase=true}|${message}|${logger}|${callsite:filename=true}|${exception:format=stackTrace}"
            };
            return consoleTarget;
        }
    }
}
