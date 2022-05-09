using gpconnect_user_portal.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using NLog.Targets;
using NLog.Targets.Splunk;

namespace gpconnect_user_portal.Core.Configuration.Logging
{
    public static class LoggingExtensions
    {
        public static IServiceCollection ConfigureLoggingServices(this IServiceCollection services, IConfiguration configuration)
        {
            var nLogConfiguration = new NLog.Config.LoggingConfiguration();

            var consoleTarget = AddConsoleTarget();
            var splunkTargetLogs = AddSplunkTarget(configuration, "LogsIndex");
            var splunkTargetWeb = AddSplunkTarget(configuration, "WebIndex");
            var splunkTargetError = AddSplunkTarget(configuration, "ErrorIndex");

            nLogConfiguration.AddTarget(consoleTarget);
            nLogConfiguration.AddTarget(splunkTargetLogs);
            nLogConfiguration.AddTarget(splunkTargetWeb);
            nLogConfiguration.AddTarget(splunkTargetError);

            nLogConfiguration.AddRule(NLog.LogLevel.Trace, NLog.LogLevel.Fatal, consoleTarget);
            nLogConfiguration.AddRuleForOneLevel(NLog.LogLevel.Info, splunkTargetLogs);
            nLogConfiguration.AddRuleForOneLevel(NLog.LogLevel.Debug, splunkTargetLogs);
            nLogConfiguration.AddRuleForOneLevel(NLog.LogLevel.Trace, splunkTargetWeb);
            nLogConfiguration.AddRule(NLog.LogLevel.Debug, NLog.LogLevel.Fatal, splunkTargetError);

            nLogConfiguration.Variables.Add("applicationVersion", ApplicationHelper.ApplicationVersion.GetAssemblyVersion());

            LogManager.Configuration = nLogConfiguration;

            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddNLog(configuration.GetSection("Logging"));
            });

            return services;
        }

        private static SplunkHttpEventCollector AddSplunkTarget(IConfiguration configuration, string indexName)
        {
            var loggingConfiguration = configuration.GetSection("Logging");

            var splunkTarget = new SplunkHttpEventCollector()
            {
                Name = $"Splunk_{indexName}",
                ServerUrl = loggingConfiguration["ServerUrl"],
                Token = loggingConfiguration["Token"],
                Index = loggingConfiguration[indexName],
                Channel = loggingConfiguration["Channel"],
                Source = loggingConfiguration["Source"],
                SourceType = loggingConfiguration["SourceType"],
                UseProxy = bool.Parse(loggingConfiguration["UseProxy"]),
                ProxyUrl = loggingConfiguration["ProxyUrl"],
                ProxyUser = loggingConfiguration["ProxyUser"],
                ProxyPassword = loggingConfiguration["ProxyPassword"]                
            };

            splunkTarget.Source = "${logger}";
            splunkTarget.SourceType = "_json";
            splunkTarget.IncludeEventProperties = false;
            splunkTarget.IncludePositionalParameters = false;
            splunkTarget.IgnoreSslErrors = false;
            splunkTarget.BatchSizeBytes = 0;
            splunkTarget.BatchSizeCount = 0;

            return splunkTarget;
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
