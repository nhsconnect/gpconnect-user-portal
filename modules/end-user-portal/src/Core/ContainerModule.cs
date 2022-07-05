using Autofac;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Data;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Data.Interfaces;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Core;

public class ContainerModule : Module
{
    protected override void Load(ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterType<TempDataProviderService>().As<ITempDataProviderService>().InstancePerLifetimeScope();
    }
}
