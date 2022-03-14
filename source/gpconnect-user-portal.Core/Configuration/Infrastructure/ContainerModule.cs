using Autofac;
using gpconnect_user_portal.Core.Configuration.Infrastructure.Authentication;
using gpconnect_user_portal.Core.Configuration.Infrastructure.Authentication.Interfaces;
using gpconnect_user_portal.DAL;
using gpconnect_user_portal.DAL.Interfaces;
using gpconnect_user_portal.Services;
using gpconnect_user_portal.Services.Interfaces;

namespace gpconnect_user_portal.Core.Configuration.Infrastructure
{
    public class ContainerModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<UserAuthentication>().As<IUserAuthentication>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<FhirRequestExecution>().As<IFhirRequestExecution>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<ConfigurationService>().As<IConfigurationService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<DataService>().As<IDataService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<ReferenceService>().As<IReferenceService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<ApplicationService>().As<IApplicationService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<CoreService>().As<ICoreService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<EmailService>().As<IEmailService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<QueryService>().As<IQueryService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<ExportService>().As<IExportService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<AggregateService>().As<IAggregateService>().InstancePerLifetimeScope();
        }
    }
}
