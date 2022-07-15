using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Data.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Constants;
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

    public async Task<IActionResult> OnGetAsync()
    {
        ClearModelState();
        if (!_tempDataProviderService.HasItems) return RedirectToPage("./Timeout");
        await PrepopulateOrganisationDetails();
        return Page();
    }

    private async Task PrepopulateOrganisationDetails()
    {
        if (IsSelectedOrganisation)
        {
            OrganisationResult = await GetOrganisationDetails(_tempDataProviderService.GetItem<string>(TempDataConstants.SELECTEDORGANISATIONODSCODE));
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
            _tempDataProviderService.PutItem(TempDataConstants.SELECTEDORGANISATIONODSCODE, SiteOdsCode);
            OrganisationResult = organisationResult;
            //ViewData[TempDataConstants.SELECTEDORGANISATION] = organisationResult;
        }
        return Page();
    }

    private Task<OrganisationResult> GetOrganisationDetails(string siteOdsCode)
    {
        return _organisationLookupService.GetOrganisationAsync(siteOdsCode);
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
