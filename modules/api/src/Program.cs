using Autofac.Extensions.DependencyInjection;
using GpConnect.NationalDataSharingPortal.Api.Core;
using GpConnect.NationalDataSharingPortal.Api.Core.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog;
using NLog.Web;
using System;

namespace GpConnect.NationalDataSharingPortal.Api;

public class Program
{
  public static void Main(string[] args)
  {
    var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

    try
    {
      logger.Debug("init main");
      CreateHostBuilder(args).Build().Run();
    }
    catch (Exception exception)
    {
      //NLog: catch setup errors
      logger.Error(exception, "Stopped program because of exception");
      throw;
    }
    finally
    {
      // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
      LogManager.Shutdown();
    }
  }

  public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
          .ConfigureWebHost((webHostBuilder) =>
          {
            WebConfigurationBuilder.ConfigureWebHost(webHostBuilder);
          })
          .UseServiceProviderFactory(new AutofacServiceProviderFactory())
          .ConfigureWebHostDefaults(webHostDefaultsBuilder =>
          {
            webHostDefaultsBuilder.UseStartup<Startup>();
            WebConfigurationBuilder.ConfigureWebHostDefaults(webHostDefaultsBuilder);
          })
          .ConfigureAppConfiguration(CustomConfigurationBuilder.AddCustomConfiguration)
          .ConfigureLogging((builderContext, logging) =>
          {
            LoggingConfigurationBuilder.AddLoggingConfiguration(builderContext, logging);
          }).UseNLog();
}
