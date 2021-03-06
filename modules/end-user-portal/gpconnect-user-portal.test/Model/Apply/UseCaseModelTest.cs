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

public class UseCaseModelTest
{
    private readonly Mock<ITempDataProviderService> _mockTempDataProviderService;
    private readonly Mock<IOptions<ApplicationParameters>> _mockOptions;

    public UseCaseModelTest()
    {
        _mockTempDataProviderService = new Mock<ITempDataProviderService>();
        _mockOptions = new Mock<IOptions<ApplicationParameters>>();
    }

    [Fact]
    public void OnPost_IfValidEntry_RedirectsToNextPage()
    {
        _mockTempDataProviderService.Setup(mtd => mtd.HasItems).Returns(true);
        var useCaseModel = new UseCaseModel(_mockOptions.Object, _mockTempDataProviderService.Object)
        {
            UseCaseDescription = "Use Case"
        };

        var result = useCaseModel.OnPost();
        Assert.IsType<RedirectToPageResult>(result);
        Assert.Contains("Agreement", ((RedirectToPageResult)result).PageName);
    }

    [Fact]
    public void OnPost_IfTempDataHasNoData_RedirectsToTimeoutPage()
    {
        _mockTempDataProviderService.Setup(mtd => mtd.HasItems).Returns(false);

        var useCaseModel = new UseCaseModel(_mockOptions.Object, _mockTempDataProviderService.Object);
        var result = useCaseModel.OnPost();
        Assert.IsType<RedirectToPageResult>(result);
        Assert.Contains("Timeout", ((RedirectToPageResult)result).PageName);
    }

    [Fact]
    public async Task OnPost_IfInvalidModel_ReturnValidationError()
    {
        var useCaseModel = new UseCaseModel(_mockOptions.Object, _mockTempDataProviderService.Object);
        useCaseModel.ModelState.AddModelError("UseCaseDescription", "You must enter a value for Use Case Description");

        var result = useCaseModel.OnPost();
        Assert.StrictEqual(ModelValidationState.Invalid, useCaseModel.ModelState.GetFieldValidationState("UseCaseDescription"));
        Assert.IsType<PageResult>(result);
        Assert.True(useCaseModel.ModelState.ErrorCount > 0);
    }

    [Fact]
    public async Task OnPost_TempDataPopulated_WithExpectedValues()
    {
        _mockTempDataProviderService.Setup(mtd => mtd.HasItems).Returns(true);

        var useCaseDescription = "Example Use Case Description";

        _mockTempDataProviderService.Setup(mtdps => mtdps.GetItem<string>(TempDataConstants.USECASEDESCRIPTION)).Returns(useCaseDescription);

        var useCaseModel = new UseCaseModel(_mockOptions.Object, _mockTempDataProviderService.Object) { UseCaseDescription = useCaseDescription };

        var result = useCaseModel.OnPost();

        _mockTempDataProviderService.Verify(mtdps => mtdps.PutItem(TempDataConstants.USECASEDESCRIPTION, useCaseDescription), Times.Once);
    }
}
