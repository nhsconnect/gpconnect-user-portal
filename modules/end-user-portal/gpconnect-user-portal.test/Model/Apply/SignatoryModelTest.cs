using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Data.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Apply;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Test.Pages.Apply;

public class SignatoryModelTest
{
    private readonly Mock<ITempDataProviderService> _mockTempDataProviderService;
    private readonly Mock<IOptions<ApplicationParameters>> _mockOptions;

    public SignatoryModelTest()
    {
        _mockTempDataProviderService = new Mock<ITempDataProviderService>();
        _mockOptions = new Mock<IOptions<ApplicationParameters>>();        
    }

    [Fact]
    public void OnPost_IfValidEntry_RedirectsToNextPage()
    {
        var signatoryModel = new SignatoryModel(_mockOptions.Object, _mockTempDataProviderService.Object)
        {
            SignatoryName = "Name",
            SignatoryRole = "Role",
            SignatoryEmail = "Email"
        };

        var result = signatoryModel.OnPost();
        Assert.IsType<RedirectToPageResult>(result);
    }
    
    [Fact]
    public void OnPost_IfTempDataHasNoData_RedirectsToTimeoutPage()
    {
        _mockTempDataProviderService.Setup(mtd => mtd.HasItems).Returns(false);

        var signatoryModel = new SignatoryModel(_mockOptions.Object, _mockTempDataProviderService.Object);
        var result = signatoryModel.OnPost();
        Assert.IsType<RedirectToPageResult>(result);
    }

    [Fact]
    public async Task OnPost_IfInvalidModel_ReturnValidationError()
    {
        var signatoryModel = new SignatoryModel(_mockOptions.Object, _mockTempDataProviderService.Object);
        signatoryModel.ModelState.AddModelError("Signatory Name", "You must enter a value for Signatory Name");

        var result = signatoryModel.OnPost();
        Assert.IsType<PageResult>(result);
        Assert.True(signatoryModel.ModelState.ErrorCount > 0);
    }
}
