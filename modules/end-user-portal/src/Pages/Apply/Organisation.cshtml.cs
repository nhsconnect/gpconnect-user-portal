using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Data.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Apply;

public partial class OrganisationModel : BaseModel
{
    private readonly ITempDataProviderService _tempDataProviderService;
    private readonly IOrganisationLookupService _organisationLookupService;

    public OrganisationModel(IOptions<ApplicationParameters> applicationParameters, IOrganisationLookupService organisationLookupService, ITempDataProviderService tempDataProviderService) : base(applicationParameters)
    {
        _tempDataProviderService = tempDataProviderService;
        _organisationLookupService = organisationLookupService;
    }

    public IActionResult OnGetAsync()
    {
        ClearModelState();
        if (!_tempDataProviderService.HasItems) return RedirectToPage("./Timeout");
        PrepopulateOrganisationDetails();
        return Page();
    }

    private void PrepopulateOrganisationDetails()
    {
        if (_tempDataProviderService.GetItem<OrganisationResult>("Organisation") != null)
        {
            OrganisationResult = _tempDataProviderService.GetItem<OrganisationResult>("Organisation");
            SiteOdsCode = OrganisationResult.OdsCode;
            OrganisationFound = true;
        }
    }

    public async Task<IActionResult> OnPostFindOrganisationAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (!_tempDataProviderService.HasItems) return RedirectToPage("./Timeout");

        var organisationResult = await GetOrganisationDetails(SiteOdsCode);
        if (organisationResult != null)
        {
            OrganisationFound = true;
            OrganisationResult = organisationResult;
            _tempDataProviderService.PutItem("Organisation", organisationResult);
        }
        return Page();
    }

    private async Task<OrganisationResult?> GetOrganisationDetails(string siteOdsCode)
    {
        var organisationResult = await _organisationLookupService.GetOrganisationAsync(siteOdsCode);
        return organisationResult;
    }

    public IActionResult OnPostNextAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (!_tempDataProviderService.HasItems) return RedirectToPage("./Timeout");

        return RedirectToPage("./Signatory");
    }

    private void ClearModelState()
    {
        ModelState.ClearValidationState("SiteOdsCode");
    }
}
