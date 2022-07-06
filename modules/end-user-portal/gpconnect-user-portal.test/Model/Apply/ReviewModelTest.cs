using System.Threading.Tasks;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Data.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Apply;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Test.Pages.Apply;

public class ReviewModelTest
{
    private readonly Mock<ITempDataProviderService> _mockTempDataProviderService;
    private readonly Mock<IAgreementService> _mockAgreementService;
    private readonly Mock<IOptions<ApplicationParameters>> _mockOptions;

    public ReviewModelTest()
    {
        _mockTempDataProviderService = new Mock<ITempDataProviderService>();
        _mockAgreementService = new Mock<IAgreementService>();
        _mockOptions = new Mock<IOptions<ApplicationParameters>>();        
    }

    [Fact]
    public async Task OnGet_IfTempDataHasNoData_RedirectsToTimeoutPage()
    {
        _mockTempDataProviderService.Setup(mtd => mtd.HasItems).Returns(false);

        var reviewModel = new ReviewModel(_mockOptions.Object, _mockTempDataProviderService.Object, _mockAgreementService.Object);
        var result = await reviewModel.OnPost();
        Assert.IsType<RedirectToPageResult>(result);
        Assert.Contains("Timeout", ((RedirectToPageResult)result).PageName);
    }

    [Fact]
    public async Task OnPost_IfTempDataHasEntries_RedirectsToConfirmationPage()
    {
        _mockTempDataProviderService.Setup(mtd => mtd.HasItems).Returns(true);

        var reviewModel = new ReviewModel(_mockOptions.Object, _mockTempDataProviderService.Object, _mockAgreementService.Object);
        var result = await reviewModel.OnPost();
        Assert.IsType<RedirectToPageResult>(result);
        Assert.Contains("Confirmation", ((RedirectToPageResult)result).PageName);
    }

    [Fact]
    public async Task OnPost_IfTempDataHasNoData_RedirectsToTimeoutPage()
    {
        _mockTempDataProviderService.Setup(mtd => mtd.HasItems).Returns(false);
        var reviewModel = new ReviewModel(_mockOptions.Object, _mockTempDataProviderService.Object, _mockAgreementService.Object);
        var result = await reviewModel.OnPost();
        Assert.IsType<RedirectToPageResult>(result);
        Assert.Contains("Timeout", ((RedirectToPageResult)result).PageName);
    }
}
