using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Data;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Data.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices;
using Microsoft.AspNetCore.Mvc;
using static GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.OrganisationLookupService;
using static GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.SiteService;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        services.AddSession(s =>
        {
            s.Cookie.Name = ".GpConnect.NationalDataSharingPortal.Session";
            s.IdleTimeout = TimeSpan.FromMinutes(30);
            s.Cookie.HttpOnly = false;
            s.Cookie.IsEssential = true;
        });

        services.Configure<CookiePolicyOptions>(options =>
        {
            options.CheckConsentNeeded = context => true;
            options.MinimumSameSitePolicy = SameSiteMode.None;
        });

        services.Configure<CookieTempDataProviderOptions>(options =>
        {
            options.Cookie.IsEssential = true;
            options.Cookie.Name = ".GpConnect.NationalDataSharingPortal.TempDataCookie";
            options.Cookie.Expiration = TimeSpan.FromMinutes(30);
        });

        services.AddOptions();
        services.Configure<ApplicationParameters>(configuration.GetSection("ApplicationParameters"));
        services.Configure<ResultPageConfig>(configuration.GetSection("Results"));
        services.Configure<SiteServiceConfig>(configuration.GetSection("SiteApi"));
        services.Configure<OrganisationLookupServiceConfig>(configuration.GetSection("OrganisationApi"));

        services.AddHsts(options =>
        {
            options.IncludeSubDomains = true;
            options.MaxAge = TimeSpan.FromDays(730);
        });

        services.AddResponseCaching();
        services.AddResponseCompression();
        services.AddHttpContextAccessor();

        services.AddHealthChecks();

        var builder = services.AddRazorPages();
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        if (environment == "Development")
        {
            builder.AddRazorRuntimeCompilation();
        }

        services.AddAntiforgery(options =>
        {
            options.SuppressXFrameOptionsHeader = true;
            options.Cookie.HttpOnly = false;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.SameSite = SameSiteMode.None;
        });

        services.AddHttpClientServices(configuration, env);
        AddDependentServices(services);

        return services;
    }

    private static void AddDependentServices(IServiceCollection services)
    {
        services.AddScoped<ITempDataProviderService, TempDataProviderService>();
    }
}
