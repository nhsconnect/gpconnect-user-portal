using Autofac;
using gpconnect_user_portal.DAL;
using gpconnect_user_portal.Framework.Configuration;
using gpconnect_user_portal.Framework.Configuration.Infrastructure;
using gpconnect_user_portal.Framework.Configuration.Infrastructure.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace gpconnect_user_portal
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

            services.ConfigureApplicationServices();
            services.ConfigureLoggingServices(_configuration);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.ConfigureContainer();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureApplicationBuilderServices(env);
        }
    }
}
