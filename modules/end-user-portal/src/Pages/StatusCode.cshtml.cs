using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages;

public partial class StatusCodeModel : BaseModel
{
    protected ILogger<StatusCodeModel> _logger;

    public StatusCodeModel(IOptions<ApplicationParameters> applicationParameters, ILogger<StatusCodeModel> logger) : base(applicationParameters)
    {
        _logger = logger;
    }

    public IActionResult OnGet(int statusCode = 404)
    {
        var feature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
        OriginalPath = feature?.OriginalPath;

        ResponseStatusCode = statusCode;
        ReasonPhrase = ReasonPhrases.GetReasonPhrase(statusCode);
        _logger.LogError($"Status code {ResponseStatusCode} thrown. Reason phrase is {ReasonPhrase}");

        switch (statusCode)
        {
            case 404:
                ResponseStatusCodePage = "404";
                break;
            default:
                ResponseStatusCodePage = "Non404";
                break;
        }

        return Page();
    }
}
