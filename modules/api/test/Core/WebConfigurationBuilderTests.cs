using GpConnect.NationalDataSharingPortal.Api.Core;
using Microsoft.AspNetCore.Hosting;
using System;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Core;

public static class WebConfigurationBuilderTests
{
    [Fact]
    public static void CallConfigureWebHostDefaults_WithNullWebHostDefaultsBuilder_ThrowsArgumentNullException()
    {
        Assert.Throws<NullReferenceException>(() => WebConfigurationBuilder.ConfigureWebHostDefaults(default(IWebHostBuilder)));
    }

    [Fact]
    public static void CallConfigureWebHostDefaults_WithNullWebHostBuilder_ThrowsArgumentNullException()
    {
        Assert.Throws<NullReferenceException>(() => WebConfigurationBuilder.ConfigureWebHost(default(IWebHostBuilder)));
    }
}
