using Autofac;

namespace GpConnect.NationalDataSharingPortal.Api.Core
{
    public static class ContainerExtensions
    {
        public static void ConfigureContainer(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule(new ContainerModule());
        }
    }
}
