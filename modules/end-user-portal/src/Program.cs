using GpConnect.NationalDataSharingPortal.EndUserPortal.Core;
using NLog;
using NLog.Web;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal;

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
          .ConfigureWebHost(o =>
          {
            o.CaptureStartupErrors(true).UseSetting("detailedErrors", "");
          })
          .ConfigureWebHostDefaults(webBuilder =>
          {
            webBuilder.UseStartup<Startup>();
            webBuilder.UseKestrel(options =>
            {
              options.AddServerHeader = false;
            });
          }).ConfigureAppConfiguration(CustomConfigurationBuilder.AddCustomConfiguration)
          .ConfigureLogging((builderContext, logging) =>
          {
            logging.ClearProviders();
            logging.AddConfiguration(builderContext.Configuration.GetSection("Logging"));
          }).UseNLog();  
}