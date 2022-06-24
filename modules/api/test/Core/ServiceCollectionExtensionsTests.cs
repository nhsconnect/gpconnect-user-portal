using GpConnect.NationalDataSharingPortal.Api.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Core;

public static class ServiceCollectionExtensionsTests
{
    [Fact]
    public static void CannotCallConfigureApplicationServicesWithNullParameters()
    {
        Assert.Throws<ArgumentNullException>(() => default(IServiceCollection).ConfigureApplicationServices(default(IConfiguration), default(IWebHostEnvironment)));
    }
}
