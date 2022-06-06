using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpRequestHandler;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Core;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
  {
    services.AddSession(s =>
    {
      s.Cookie.Name = ".GpConnect.NationalDataSharingPortal.EndUserPortal.Session";
      s.IdleTimeout = new TimeSpan(0, 30, 0);
      s.Cookie.HttpOnly = false;
      s.Cookie.IsEssential = true;
    });

    services.Configure<CookiePolicyOptions>(options =>
    {
      options.CheckConsentNeeded = context => true;
      options.MinimumSameSitePolicy = SameSiteMode.None;
    });

    services.AddOptions();
    services.Configure<ApplicationParameters>(configuration.GetSection("ApplicationParameters"));

    services.AddHsts(options =>
    {
      options.IncludeSubDomains = true;
      options.MaxAge = TimeSpan.FromDays(730);
    });

    services.AddResponseCaching();
    services.AddResponseCompression();
    services.AddHttpContextAccessor();

    services.AddHealthChecks();

    services.AddRazorPages(options =>
    {      
    });

    services.AddAntiforgery(options =>
    {
      options.SuppressXFrameOptionsHeader = true;
      options.Cookie.HttpOnly = false;
      options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
      options.Cookie.SameSite = SameSiteMode.None;
    });

    HttpClientExtensions.AddHttpClientServices(services, configuration, env);

    return services;
  }  
}