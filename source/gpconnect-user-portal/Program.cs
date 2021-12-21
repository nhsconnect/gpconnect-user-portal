using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog.Web;
using Infrastructure = gpconnect_user_portal.Core.Configuration.Infrastructure;
using Logging = gpconnect_user_portal.Core.Configuration.Logging;

namespace gpconnect_user_portal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHost((webHostBuilder) =>
                {
                    Infrastructure.WebConfigurationBuilder.ConfigureWebHost(webHostBuilder);                    
                })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webHostDefaultsBuilder =>
                {                    
                    webHostDefaultsBuilder.UseStartup<Startup>();
                    Infrastructure.WebConfigurationBuilder.ConfigureWebHostDefaults(webHostDefaultsBuilder);
                })
                .ConfigureAppConfiguration(Infrastructure.CustomConfigurationBuilder.AddCustomConfiguration)
                .ConfigureLogging((builderContext, logging) =>
                {
                    Logging.LoggingConfigurationBuilder.AddLoggingConfiguration(builderContext, logging);
                })
                .UseNLog();
    }
}
