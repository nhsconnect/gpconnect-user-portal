using gpconnect_user_portal.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog;
using NLog.Targets;
using NLog.Targets.Splunk;
using System;

namespace gpconnect_user_portal.Core.Configuration.Logging
{
    public static class LoggingExtensions
    {
        public static IServiceCollection ConfigureLoggingServices(this IServiceCollection services, IConfiguration configuration)
        {
            var nLogConfiguration = new NLog.Config.LoggingConfiguration();

            var consoleTarget = AddConsoleTarget();
            var splunkTarget = AddSplunkTarget(configuration);

            nLogConfiguration.AddRule(NLog.LogLevel.Trace, NLog.LogLevel.Fatal, consoleTarget);
            nLogConfiguration.AddRule(NLog.LogLevel.Debug, NLog.LogLevel.Fatal, splunkTarget);

            nLogConfiguration.AddTarget("Console", consoleTarget);
            nLogConfiguration.AddTarget("Splunk", splunkTarget);

            nLogConfiguration.Variables.Add("applicationVersion", ApplicationHelper.ApplicationVersion.GetAssemblyVersion());

            LogManager.Configuration = nLogConfiguration;

            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddConfiguration(configuration.GetSection("Logging"));
            });

            return services;
        }

        private static SplunkHttpEventCollector AddSplunkTarget(IConfiguration configuration)
        {
            var loggingConfiguration = configuration.GetSection("Logging");

            var splunkTarget = new SplunkHttpEventCollector()
            {
                Name = "Splunk",
                ServerUrl = loggingConfiguration["ServerUrl"],
                Token = loggingConfiguration["Token"],
                Index = loggingConfiguration["Index"],
                Channel = loggingConfiguration["Channel"],
                Source = loggingConfiguration["Source"],
                SourceType = loggingConfiguration["SourceType"],
                UseProxy = bool.Parse(loggingConfiguration["UseProxy"]),
                ProxyUrl = loggingConfiguration["ProxyUrl"],
                ProxyUser = loggingConfiguration["ProxyUser"],
                ProxyPassword = loggingConfiguration["ProxyPassword"]                
            };

            splunkTarget.Source = "${message}|${logger}";
            splunkTarget.SourceType = "_json";
            splunkTarget.IncludeEventProperties = true;
            splunkTarget.IncludePositionalParameters = false;
            splunkTarget.IgnoreSslErrors = true;
            splunkTarget.BatchSizeBytes = 0;
            splunkTarget.BatchSizeCount = 0;
            splunkTarget.Index = "logs_ndsp_local";

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
