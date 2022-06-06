using GpConnect.NationalDataSharingPortal.Api.Dal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;

namespace GpConnect.NationalDataSharingPortal.Api.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddOptions();
            services.Configure<ConnectionStrings>(configuration.GetSection("ConnectionStrings"));

            services.AddHsts(options =>
            {
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(730);
            });

            services.AddResponseCaching();
            services.AddResponseCompression();
            services.AddHttpContextAccessor();

            services.AddHealthChecks();
            services.AddControllers();
            services.AddSwaggerGen();

            services.AddAntiforgery(options =>
            {
                options.SuppressXFrameOptionsHeader = true;
                options.Cookie.HttpOnly = false;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.None;
            });

            return services;
        }
    }

}
