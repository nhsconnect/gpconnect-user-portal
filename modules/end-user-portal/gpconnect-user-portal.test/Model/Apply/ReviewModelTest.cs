using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Data.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Apply;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Test.Pages.Apply;

public class ReviewModelTest
{
    private readonly Mock<ITempDataProviderService> _mockTempDataProviderService;
    private readonly Mock<IOptions<ApplicationParameters>> _mockOptions;

    public ReviewModelTest()
    {
        _mockTempDataProviderService = new Mock<ITempDataProviderService>();
        _mockOptions = new Mock<IOptions<ApplicationParameters>>();        
    }

    [Fact]
    public void OnGet_IfTempDataHasNoData_RedirectsToTimeoutPage()
    {
        _mockTempDataProviderService.Setup(mtd => mtd.HasItems).Returns(false);

        var reviewModel = new ReviewModel(_mockOptions.Object, _mockTempDataProviderService.Object);
        var result = reviewModel.OnPost();
        Assert.IsType<RedirectToPageResult>(result);
        Assert.Contains("Timeout", ((RedirectToPageResult)result).PageName);
    }

    [Fact]
    public void OnPost_IfTempDataHasEntries_RedirectsToConfirmationPage()
    {
        _mockTempDataProviderService.Setup(mtd => mtd.HasItems).Returns(true);

        var reviewModel = new ReviewModel(_mockOptions.Object, _mockTempDataProviderService.Object);
        var result = reviewModel.OnPost();
        Assert.IsType<RedirectToPageResult>(result);
        Assert.Contains("Confirmation", ((RedirectToPageResult)result).PageName);
    }

    [Fact]
    public void OnPost_IfTempDataHasNoData_RedirectsToTimeoutPage()
    {
        _mockTempDataProviderService.Setup(mtd => mtd.HasItems).Returns(false);
        var reviewModel = new ReviewModel(_mockOptions.Object, _mockTempDataProviderService.Object);
        var result = reviewModel.OnPost();
        Assert.IsType<RedirectToPageResult>(result);
        Assert.Contains("Timeout", ((RedirectToPageResult)result).PageName);
    }
}
