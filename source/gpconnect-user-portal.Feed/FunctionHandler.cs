using Amazon.Lambda.Core;
using gpconnect_user_portal.DAL.Interfaces;
using gpconnect_user_portal.Services;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace gpconnect_user_portal.Feed
{
    public class Function
    {
        public IConfigurationService ConfigurationService { get; }
        public IDataService DataService { get; }

        public Function()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            DataService = serviceProvider.GetService<IDataService>();
            ConfigurationService = serviceProvider.GetService<IConfigurationService>();
        }

        public void FunctionHandler(ILambdaContext context)
        {
            //var ldapQueryConfiguration = ConfigurationService.GetReferenceApiQuery().Result;

            var attributes = new string[] { "nhsassvcia", "nhsidcode", "nhsmhsmanufacturerorg", "nhsmhspartykey", "nhsproductname", "nhsproductversion", "uniqueidentifier" };
            var ldapResponse = LdapRequestExecution.ExecuteLdapQuery(
                "ou=services,o=nhs", "(&(nhsassvcia=urn:nhs:names:services:gpconnect*)(objectclass=nhsas))", attributes);

            try
            {
                
            }
            catch
            {
                throw;
            }
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IConfigurationService, ConfigurationService>();
        }
    }
}
