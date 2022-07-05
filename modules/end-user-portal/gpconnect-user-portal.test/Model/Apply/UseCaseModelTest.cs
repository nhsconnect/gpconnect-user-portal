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
        var useCaseModel = new UseCaseModel(_mockOptions.Object, _mockTempDataProviderService.Object)
        {
            UseCaseDescription = "Use Case"
        };

        var result = useCaseModel.OnPost();
        Assert.IsType<RedirectToPageResult>(result);
    }
    
    [Fact]
    public void OnPost_IfTempDataHasNoData_RedirectsToTimeoutPage()
    {
        _mockTempDataProviderService.Setup(mtd => mtd.HasItems).Returns(false);

        var useCaseModel = new UseCaseModel(_mockOptions.Object, _mockTempDataProviderService.Object);
        var result = useCaseModel.OnPost();
        Assert.IsType<RedirectToPageResult>(result);
    }

    [Fact]
    public async Task OnPost_IfInvalidModel_ReturnValidationError()
    {
        var useCaseModel = new UseCaseModel(_mockOptions.Object, _mockTempDataProviderService.Object);
        useCaseModel.ModelState.AddModelError("Use Case Description", "You must enter a value for Use Case Description");

        var result = useCaseModel.OnPost();
        Assert.IsType<PageResult>(result);
        Assert.True(useCaseModel.ModelState.ErrorCount > 0);
    }
}
