using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace gpconnect_user_portal.Core.Configuration.Logging
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
