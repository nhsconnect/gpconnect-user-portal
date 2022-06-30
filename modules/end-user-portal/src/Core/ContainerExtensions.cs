using Autofac;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Core
{
    public static class ContainerExtensions
    {
        public static void ConfigureContainer(this ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule(new ContainerModule());
        }
    }
}
