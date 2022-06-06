namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Logging;

public static class LoggingConfigurationBuilder
{
  public static void AddLoggingConfiguration(HostBuilderContext builderContext, ILoggingBuilder logging)
  {
    logging.ClearProviders();
    logging.SetMinimumLevel(LogLevel.Trace);
    logging.AddConfiguration(builderContext.Configuration.GetSection("Logging"));
  }
}
