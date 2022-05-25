using Autofac;
using GpConnect.NationalDataSharingPortal.Api.Core;
using GpConnect.NationalDataSharingPortal.Api.Core.Logging;
using GpConnect.NationalDataSharingPortal.Api.Core.Mapping;
using GpConnect.NationalDataSharingPortal.Api.Dal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GpConnect.NationalDataSharingPortal.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            if (configuration.GetConnectionString(ConnectionStrings.DefaultConnection) == null) throw new ArgumentNullException(nameof(ConnectionStrings.DefaultConnection));

            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }        

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddHttpContextAccessor();
            
            services.ConfigureApplicationServices(_configuration, _webHostEnvironment);
            services.ConfigureLoggingServices(_configuration);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.ConfigureContainer();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureApplicationBuilderServices(env);
            MappingExtensions.ConfigureMappingServices();
        }
    }
}
