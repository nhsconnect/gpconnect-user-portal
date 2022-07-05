using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Apply;

public partial class OrganisationModel : BaseModel
{
    private readonly IOrganizationLookupService _orgLookupService;

    public OrganisationModel(IOptions<ApplicationParameters> applicationParameters, IOrganizationLookupService orgLookupService) : base(applicationParameters)
    {
        _orgLookupService = orgLookupService;
    }

    public IActionResult OnGetAsync()
    {
        ClearModelState();
        return Page();
    }
    
    public async Task<IActionResult> OnPostFindOrganisationAsync()
    {        
        if (!ModelState.IsValid)
        {
            return Page();
        }
        OrganisationResult = await _orgLookupService.GetOrganizationAsync(SiteOdsCode);
        return Page();
    }

    public IActionResult OnPostNextAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        return Redirect("./Signatory");
    }

    private void ClearModelState()
    {
        ModelState.ClearValidationState("SiteOdsCode");
    }
}
