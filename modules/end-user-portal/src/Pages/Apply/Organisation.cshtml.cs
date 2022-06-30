using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Apply;

public partial class OrganisationModel : BaseModel
{
    public OrganisationModel(IOptions<ApplicationParameters> applicationParameters) : base(applicationParameters)
    {
        _orgLookupService = orgLookupService;
    }

    public IActionResult OnGetAsync()
    {
        ClearModelState();
        return Page();
    }
    
    public IActionResult OnPostFindOrganisationAsync()
    {        
        if (!ModelState.IsValid)
        {
            return Page();
        }
        OrganisationResult = GetOrganisationDetails(SiteOdsCode);
        return Page();
    }

    private OrganisationResult GetOrganisationDetails(string siteOdsCode)
    {
        var organisationResult = new OrganisationResult()
        {
            OdsCode = siteOdsCode,
            Name = "TESTVALE SURGERY",
            Address = new OrganisationAddress()
            {
                AddressLines = new List<string> { "12 SALISBURY ROAD", "TOTTON" }, City = "SOUTHAMPTON", County = "HAMPSHIRE", Country = "ENGLAND", Postcode = "SO40 3PY"
            }
        };
        return organisationResult;
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
