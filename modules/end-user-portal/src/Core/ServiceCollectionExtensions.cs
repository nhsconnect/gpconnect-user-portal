using GpConnect.NationalDataSharingPortal.EndUserPortal.Builders;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Builders.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Data;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Data.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices;
using Microsoft.AspNetCore.Mvc;
using static GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.AgreementService;
using static GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.FeedbackService;
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
            options.MinimumSameSitePolicy = SameSiteMode.Lax;
            options.Secure = CookieSecurePolicy.SameAsRequest;
            options.ConsentCookie.SameSite = SameSiteMode.Lax;
            options.ConsentCookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
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
        services.Configure<AgreementServiceConfig>(configuration.GetSection("AgreementApi"));
        services.Configure<FeedbackServiceConfig>(configuration.GetSection("FeedbackApi"));
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
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            options.Cookie.SameSite = SameSiteMode.Lax;
        });

        services.AddHttpClientServices(configuration, env);
        services.AddDependentServices();

        return services;
    }

    private static void AddDependentServices(this IServiceCollection services)
    {
        services.AddScoped<ITempDataProviderService, TempDataProviderService>();
        services.AddSingleton<IOrganisationBuilder, OrganisationBuilder>();
        services.AddSingleton<IInteractionsBuilder, InteractionsBuilder>();
        services.AddSingleton<ISignatoryBuilder, SignatoryBuilder>();
        services.AddSingleton<IAgreementInformationBuilder, AgreementInformationBuilder>();
        services.AddSingleton<ISupplierBuilder, SupplierBuilder>();
    }
}
