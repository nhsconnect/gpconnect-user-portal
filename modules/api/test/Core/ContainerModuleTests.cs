using Autofac.Extras.Moq;
using GpConnect.NationalDataSharingPortal.Api.Core;
using GpConnect.NationalDataSharingPortal.Api.Service;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Core;

public class ContainerModuleTests
{
    [Fact]
    public void CanConstruct()
    {
        var instance = new ContainerModule();
        Assert.NotNull(instance);
    }
    
    [Fact]
    public static void CanCallConfigureModuleForServices()
    {
        using (var mock = AutoMock.GetLoose())
        {
            Assert.NotNull(mock.Create<TransparencySiteService>());
            Assert.NotNull(mock.Create<UserService>());
            Assert.NotNull(mock.Create<SupplierService>());
            Assert.NotNull(mock.Create<ProductService>());
            Assert.NotNull(mock.Create<CcgService>());
            Assert.NotNull(mock.Create<CareSettingService>());
        }
    }

}
