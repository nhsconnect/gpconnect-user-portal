using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Data.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Apply;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Test.Pages.Apply;

public class OrganisationModelTest
{
    private readonly Mock<IOrganisationLookupService> _mockOrganisationService;
    private readonly Mock<ITempDataProviderService> _mockTempDataProviderService;
    private readonly Mock<IOptions<ApplicationParameters>> _mockOptions;

    public OrganisationModelTest()
    {
        _mockOrganisationService = new Mock<IOrganisationLookupService>();
        _mockTempDataProviderService = new Mock<ITempDataProviderService>();
        _mockOptions = new Mock<IOptions<ApplicationParameters>>();        
    }

    [Fact]
    public async Task OnPostFind_IfValidEntry_ReturnsPageResult()
    {
        _mockTempDataProviderService.Setup(mtd => mtd.HasItems).Returns(true);
        var organisationModel = new OrganisationModel(_mockOptions.Object, _mockOrganisationService.Object, _mockTempDataProviderService.Object)
        {
            SiteOdsCode = "J82132"
        };

        var result = await organisationModel.OnPostFindOrganisationAsync();
        Assert.IsType<PageResult>(result);
    }

    [Fact]
    public void OnPost_IfValidEntry_RedirectsToNextPage()
    {
        _mockTempDataProviderService.Setup(mtd => mtd.HasItems).Returns(true);
        var organisationModel = new OrganisationModel(_mockOptions.Object, _mockOrganisationService.Object, _mockTempDataProviderService.Object)
        {
            SiteOdsCode = "J82132"
        };

        var result = organisationModel.OnPostNextAsync();
        Assert.IsType<RedirectToPageResult>(result);
        Assert.Contains("Signatory", ((RedirectToPageResult)result).PageName);
    }

    [Fact]
    public async Task OnPostFind_IfTempDataHasNoEntries_RedirectsToTimeoutPage()
    {
        _mockTempDataProviderService.Setup(mtd => mtd.HasItems).Returns(false);
        var organisationModel = new OrganisationModel(_mockOptions.Object, _mockOrganisationService.Object, _mockTempDataProviderService.Object);

        var result = await organisationModel.OnPostFindOrganisationAsync();
        Assert.IsType<RedirectToPageResult>(result);
        Assert.Contains("Timeout", ((RedirectToPageResult)result).PageName);
    }

    [Fact]
    public void OnPost_IfTempDataHasNoData_RedirectsToTimeoutPage()
    {
        _mockTempDataProviderService.Setup(mtd => mtd.HasItems).Returns(false);

        var organisationModel = new OrganisationModel(_mockOptions.Object, _mockOrganisationService.Object, _mockTempDataProviderService.Object);
        var result = organisationModel.OnPostNextAsync();
        Assert.IsType<RedirectToPageResult>(result);
        Assert.Contains("Timeout", ((RedirectToPageResult)result).PageName);
    }

    [Fact]
    public async Task OnPostFind_IfInvalidModel_ReturnValidationError()
    {
        var organisationModel = new OrganisationModel(_mockOptions.Object, _mockOrganisationService.Object, _mockTempDataProviderService.Object);
        organisationModel.ModelState.AddModelError("SiteOdsCode", "You must enter a value for Site ODS Code");

        var result = await organisationModel.OnPostFindOrganisationAsync();
        Assert.IsType<PageResult>(result);
        Assert.True(organisationModel.ModelState.ErrorCount > 0);
    }
}
