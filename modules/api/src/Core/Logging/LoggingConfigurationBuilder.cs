using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GpConnect.NationalDataSharingPortal.Api.Core.Logging
{
    public static class LoggingConfigurationBuilder
    {
        public static void AddLoggingConfiguration(HostBuilderContext builderContext, ILoggingBuilder logging)
        {
            logging.ClearProviders();
            logging.SetMinimumLevel(LogLevel.Trace);
            logging.AddConfiguration(builderContext.Configuration.GetSection("Logging"));          
        }
    }
}
