using gpconnect_user_portal.Core.Configuration.Infrastructure.Authentication;
using gpconnect_user_portal.Core.Configuration.Infrastructure.Logging.Interfaces;
using gpconnect_user_portal.Core.Configuration.Logging;
using gpconnect_user_portal.DTO.Response.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;

namespace gpconnect_user_portal.Core.Configuration.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            services.AddSession(s =>
            {
                s.Cookie.Name = ".GpConnectEnablementTeamPortal.Session";
                s.IdleTimeout = new TimeSpan(0, 30, 0);
                s.Cookie.HttpOnly = false;
                s.Cookie.IsEssential = true;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

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
                options.Conventions.AuthorizeFolder("/Private", "MustHaveAuthorisedUserStatus");
                options.Conventions.AllowAnonymousToFolder("/Public");
                options.Conventions.AddPageRoute("/Private/Lookup/Supplier/Index", "/Lookup/Supplier");
                options.Conventions.AddPageRoute("/Private/Lookup/Supplier/AddProduct", "/Lookup/Supplier/AddProduct/{supplierId}");
                options.Conventions.AddPageRoute("/Private/Lookup/AddLookup", "/Lookup/AddLookup/{lookupTypeId}");
                options.Conventions.AddPageRoute("/Private/Lookup/Supplier/AddSupplier", "/Lookup/Supplier/AddSupplier");
                options.Conventions.AddPageRoute("/Private/Lookup/Detail", "/Lookup/{lookupType}");
                options.Conventions.AddPageRoute("/Private/Outstanding/Index", "/Outstanding");
                options.Conventions.AddPageRoute("/Private/Outstanding/Detail", "/Outstanding/Detail/{siteIdentifier}");
                options.Conventions.AddPageRoute("/Private/Completed/Index", "/Completed");
                options.Conventions.AddPageRoute("/Private/Completed/Detail", "/Completed/Detail/{siteIdentifier}");
                options.Conventions.AddPageRoute("/Private/Users", "/Users");
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("MustHaveAuthorisedUserStatus", policy => policy.Requirements.Add(new AuthorisedUserRequirement()));
            });

            services.AddSingleton<IAuthorizationHandler, AuthorisedUserHandler>();

            services.AddAntiforgery(options =>
            {
                options.SuppressXFrameOptionsHeader = true;
                options.Cookie.HttpOnly = false;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.None;
            });

            services.Configure<General>(configuration.GetSection("General"));
            services.Configure<Reference>(configuration.GetSection("Reference"));
            services.Configure<Sso>(configuration.GetSection("SingleSignOn"));
            services.Configure<DTO.Response.Configuration.Logging>(configuration.GetSection("Logging"));
            services.Configure<Email>(configuration.GetSection("Email"));

            services.ConfigureNLogService();

            HttpClientExtensions.AddHttpClientServices(services, env);
            SmtpClientExtensions.AddSmtpClientServices(services);

            return services;
        }

        public static void ConfigureNLogService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddSingleton<IWebLoggerManager, WebLoggerManager>();
        }
    }

}
