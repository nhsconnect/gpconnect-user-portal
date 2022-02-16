using Autofac;
using gpconnect_user_portal.Core.Configuration.Infrastructure;
using gpconnect_user_portal.Core.Configuration.Logging;
using gpconnect_user_portal.Core.Configuration.Infrastructure.Authentication;
using gpconnect_user_portal.DAL;

namespace gpconnect_user_portal.Admin
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

            var authenticationExtensions = new AuthenticationExtensions(_configuration);
            authenticationExtensions.ConfigureAuthenticationServices(services);

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
        }
    }
}
