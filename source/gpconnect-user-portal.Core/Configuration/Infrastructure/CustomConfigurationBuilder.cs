using System;
using gpconnect_user_portal.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace gpconnect_user_portal.Core.Configuration.Infrastructure
{
    public static class CustomConfigurationBuilder
    {
         
        public static void AddCustomConfiguration(HostBuilderContext context, IConfigurationBuilder builder)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            builder.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);
            
            // Add support for AWS Systems Manager (SSM) for secure parameters
            if (environment != "Development")
            {
                /* /GPConnect/NDSP/ConnectionString/GPConnectEndUserPortal */
                builder.AddSystemsManager(options => {
                    options.Path = "";
                });
            }
            
            builder.AddEnvironmentVariables();

            var configuration = builder.Build();
        }
    }
}
