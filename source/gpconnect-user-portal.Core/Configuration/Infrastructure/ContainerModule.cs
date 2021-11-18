using Autofac;
using gpconnect_user_portal.DAL;
using gpconnect_user_portal.DAL.Interfaces;
using gpconnect_user_portal.Framework.Configuration.Infrastructure.Logging;
using gpconnect_user_portal.Framework.Configuration.Infrastructure.Logging.Interfaces;
using gpconnect_user_portal.Services;
using gpconnect_user_portal.Services.Interfaces;

namespace gpconnect_user_portal.Framework.Configuration.Infrastructure
{
    public class ContainerModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<DataService>().As<IDataService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<LogService>().As<ILogService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<LoggerManager>().As<ILoggerManager>().SingleInstance();
        }
    }
}
