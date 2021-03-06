using GpConnect.NationalDataSharingPortal.Api.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Core;

public static class CustomConfigurationBuilderTests
{
    [Fact]
    public static void CreatingCustomConfigurationBuilderInstance_WithNullParameters_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => CustomConfigurationBuilder.AddCustomConfiguration(default(HostBuilderContext), default(IConfigurationBuilder)));
    }
}
