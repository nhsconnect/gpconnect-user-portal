using GpConnect.NationalDataSharingPortal.Api.Controllers;
using GpConnect.NationalDataSharingPortal.Api.Core;
using GpConnect.NationalDataSharingPortal.Api.Dal;
using GpConnect.NationalDataSharingPortal.Api.Dal.Interfaces;
using GpConnect.NationalDataSharingPortal.Api.Service;
using GpConnect.NationalDataSharingPortal.Api.Service.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Core;

public static class ApplicationBuilderExtensionsTests
{
    [Fact]
    public static void CreatingApplicationBuilderServicesInstance_WithExpectedParameters_ReturnsApplicationBuilderServicesInstance()
    {
        Mock<IConfigurationSection> configurationSectionStub = new Mock<IConfigurationSection>();
        Mock<IWebHostEnvironment> webHostEnvironmentStub = new Mock<IWebHostEnvironment>();
        configurationSectionStub.Setup(x => x["DefaultConnection"]).Returns("TestConnectionString");
        Mock<IConfiguration> configurationStub = new Mock<IConfiguration>();
        configurationStub.Setup(x => x.GetSection("ConnectionStrings")).Returns(configurationSectionStub.Object);

        var services = new ServiceCollection();
        var target = new Startup(configurationStub.Object, webHostEnvironmentStub.Object);

        target.ConfigureServices(services);

        services.AddTransient<IDataService, DataService>();
        services.AddTransient<ITransparencySiteService, TransparencySiteService>();
        services.AddTransient<ICcgService, CcgService>();
        services.AddTransient<ICareSettingService, CareSettingService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<ISupplierService, SupplierService>();

        var serviceProvider = services.BuildServiceProvider();

        Assert.NotNull(serviceProvider.GetService<IDataService>());
        Assert.NotNull(serviceProvider.GetService<ITransparencySiteService>());
        Assert.NotNull(serviceProvider.GetService<ICcgService>());
        Assert.NotNull(serviceProvider.GetService<ICareSettingService>());
        Assert.NotNull(serviceProvider.GetService<IUserService>());
        Assert.NotNull(serviceProvider.GetService<IProductService>());
        Assert.NotNull(serviceProvider.GetService<ISupplierService>());
    }

    [Fact]
    public static void CreatingApplicationBuilderServicesInstance_WithNullParameters_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => default(IApplicationBuilder).ConfigureApplicationBuilderServices(default(IWebHostEnvironment)));
    }
}
