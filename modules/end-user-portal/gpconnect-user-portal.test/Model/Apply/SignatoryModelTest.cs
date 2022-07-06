using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Data.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Constants;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Apply;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        _mockTempDataProviderService.Setup(mtd => mtd.HasItems).Returns(true);

        var signatoryModel = new SignatoryModel(_mockOptions.Object, _mockTempDataProviderService.Object)
        {
            SignatoryName = "Name",
            SignatoryRole = "Role",
            SignatoryEmail = "Email"
        };

        var result = signatoryModel.OnPost();
        Assert.IsType<RedirectToPageResult>(result);
        Assert.Contains("UseCase", ((RedirectToPageResult)result).PageName);
    }
    
    [Fact]
    public void OnPost_IfTempDataHasNoData_RedirectsToTimeoutPage()
    {
        _mockTempDataProviderService.Setup(mtd => mtd.HasItems).Returns(false);

        var signatoryModel = new SignatoryModel(_mockOptions.Object, _mockTempDataProviderService.Object);
        var result = signatoryModel.OnPost();
        Assert.IsType<RedirectToPageResult>(result);
        Assert.Contains("Timeout", ((RedirectToPageResult)result).PageName);
    }

    [Fact]
    public void OnPost_IfInvalidModel_ReturnValidationError()
    {
        var signatoryModel = new SignatoryModel(_mockOptions.Object, _mockTempDataProviderService.Object);
        signatoryModel.ModelState.AddModelError("SignatoryName", "You must enter a value for Signatory Name");
        signatoryModel.ModelState.AddModelError("SignatoryEmail", "You must enter a value for Signatory Email");
        signatoryModel.ModelState.AddModelError("SignatoryRole", "You must enter a value for Signatory Role");

        var result = signatoryModel.OnPost();

        Assert.StrictEqual(ModelValidationState.Invalid, signatoryModel.ModelState.GetFieldValidationState("SignatoryName"));
        Assert.StrictEqual(ModelValidationState.Invalid, signatoryModel.ModelState.GetFieldValidationState("SignatoryEmail"));
        Assert.StrictEqual(ModelValidationState.Invalid, signatoryModel.ModelState.GetFieldValidationState("SignatoryRole"));

        Assert.IsType<PageResult>(result);
        Assert.True(signatoryModel.ModelState.ErrorCount == 3);
    }

    [Fact]
    public void OnPost_TempDataPopulated_WithExpectedValues()
    {
        _mockTempDataProviderService.Setup(mtd => mtd.HasItems).Returns(true);

        var signatoryName = "Example Name";
        var signatoryEmail = "Example Email";
        var signatoryRole = "Example Role";

        _mockTempDataProviderService.Setup(mtdps => mtdps.GetItem<string>(TempDataConstants.SIGNATORYNAME)).Returns(signatoryName);

        var useCaseModel = new SignatoryModel(_mockOptions.Object, _mockTempDataProviderService.Object) { SignatoryName = signatoryName, SignatoryEmail = signatoryEmail, SignatoryRole = signatoryRole };

        var result = useCaseModel.OnPost();

        _mockTempDataProviderService.Verify(mtdps => mtdps.PutItem(TempDataConstants.SIGNATORYNAME, signatoryName), Times.Once);
    }
}
