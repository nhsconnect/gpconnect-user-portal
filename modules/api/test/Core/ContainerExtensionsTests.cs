using Autofac;
using GpConnect.NationalDataSharingPortal.Api.Core;
using System;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Core;

public static class ContainerExtensionsTest
{    
    [Fact]
    public static void CannotCallConfigureContainerWithNullContainerBuilder()
    {
        Assert.Throws<ArgumentNullException>(() => default(ContainerBuilder).ConfigureContainer());
    }
}
