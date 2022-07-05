using Autofac.Extras.Moq;
using GpConnect.NationalDataSharingPortal.Api.Core;
using GpConnect.NationalDataSharingPortal.Api.Service;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Core;

public class ContainerModuleTests
{
    private readonly ContainerModule _sut;

    public ContainerModuleTests()
    {
        _sut = new ContainerModule();
    }

    [Fact]
    public void CallConstructor_WithExpectedParameters_ReturnsNotNull()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public static void CallConfigureModuleServicesInstance_WithValidParameters_ReturnsConfigureModuleForServicesInstance()
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
