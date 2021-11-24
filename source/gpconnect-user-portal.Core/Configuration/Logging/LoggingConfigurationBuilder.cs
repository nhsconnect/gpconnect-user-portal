using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace gpconnect_user_portal.Core.Configuration.Infrastructure
{
    public static class LoggingConfigurationBuilder
    {
        public static void AddLoggingConfiguration(HostBuilderContext builderContext, ILoggingBuilder logging)
        {
            logging.ClearProviders();
            logging.AddConfiguration(builderContext.Configuration.GetSection("Logging"));          
        }
    }
}
