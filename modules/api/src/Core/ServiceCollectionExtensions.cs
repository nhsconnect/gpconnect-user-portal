using System;
using System.Net;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using GpConnect.NationalDataSharingPortal.Api.Dal;
using GpConnect.NationalDataSharingPortal.Api.Dal.Configuration;
using GpConnect.NationalDataSharingPortal.Api.Dal.Interfaces;
using GpConnect.NationalDataSharingPortal.Api.Service;
using GpConnect.NationalDataSharingPortal.Api.Service.Interface;
using GpConnect.NationalDataSharingPortal.Api.Validators;
using GpConnect.NationalDataSharingPortal.Api.Validators.Interface;
using GpConnect.NationalDataSharingPortal.Api.Dal.Authentication.Interface;
using GpConnect.NationalDataSharingPortal.Api.Dal.Authentication;
using GpConnect.NationalDataSharingPortal.Api.Stores;

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
            services.AddTransient<IPostConfigureOptions<ConnectionStrings>, ConnectionStringsPostConfiguration>();
            services.AddSingleton<IAuthTokenGenerator, RdsAuthTokenGenerator>();

            services.AddScoped<IDataService, DataService>();
            services.AddSingleton<ITransparencySiteRequestValidator, TransparencySiteRequestValidator>();
            services.AddScoped<ITransparencySiteService, TransparencySiteService>();
            services.AddSingleton<ICareSettingRequestValidator, CareSettingRequestValidator>();
            services.AddScoped<ICareSettingService, CareSettingService>();
            services.AddScoped<ICcgService, CcgService>();
            services.AddSingleton<ISupplierRequestValidator,SupplierRequestValidator>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddSingleton<IProductRequestValidator, ProductRequestValidator>();
            services.AddScoped<IProductService,ProductService>();
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<IAgreementService, AgreementService>();
            services.AddScoped<ISiteService, SiteService>();
            services.AddSingleton<IFeedbackRequestValidator, FeedbackRequestValidator>();
            services.AddScoped<IFeedbackService, FeedbackService>();

             services.AddSingleton<ISupplierStore, SupplierStore>();

            services.AddResponseCaching();
            services.AddResponseCompression();
            services.AddHttpContextAccessor();

            services.AddHealthChecks();
            services.AddControllers();
            services.AddSwaggerGen();

            return services;
        }
    }

}
