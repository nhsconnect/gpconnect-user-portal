using gpconnect_user_portal.Core.Configuration.Infrastructure;
using gpconnect_user_portal.Core.Configuration.Mapping;
using gpconnect_user_portal.DAL;
using gpconnect_user_portal.DAL.Interfaces;
using gpconnect_user_portal.Download;
using gpconnect_user_portal.Services;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;

[assembly: FunctionsStartup(typeof(Startup))]
namespace gpconnect_user_portal.Download
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<IConfigurationService, ConfigurationService>();
            builder.Services.AddScoped<IReferenceService, ReferenceService>();
            builder.Services.AddScoped<IDataService, DataService>();
            builder.Services.AddScoped<IFhirRequestExecution, FhirRequestExecution>();
            builder.Services.AddScoped<IApplicationService, ApplicationService>();

            var config = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddConfiguration(options =>
            {
                options.ConnectionString = Environment.GetEnvironmentVariable(ConnectionStrings.DefaultConnection);
            }).Build();

            builder.Services.AddOptions();
            builder.Services.AddSingleton<IConfiguration>(config);            
        }
    }
}
