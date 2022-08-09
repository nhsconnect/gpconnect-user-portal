using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Data.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Apply;

public class IndexModel : BaseModel
{
    private readonly ITempDataProviderService _tempDataProviderService;

    public IndexModel(IOptions<ApplicationParameters> applicationParameters, ITempDataProviderService tempDataProviderService) : base(applicationParameters)
    {
        _tempDataProviderService = tempDataProviderService;
    }

    public IActionResult OnPost()
    {
        _tempDataProviderService.RemoveAll();
        return RedirectToPage("./SystemSupplier");
    }
}
