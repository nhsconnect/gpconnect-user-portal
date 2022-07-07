using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
public class ErrorModel : BaseModel
{
    protected ILogger<ErrorModel> _logger;

    public ErrorModel(IOptions<ApplicationParameters> applicationParameters, ILogger<ErrorModel> logger) : base(applicationParameters)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

        if (exceptionHandlerPathFeature != null)
        {
            var exceptionPath = exceptionHandlerPathFeature.Path;
            var exception = exceptionHandlerPathFeature.Error;

            _logger.LogError(exception, $"Error thrown at {exceptionPath}");
        }
    }
}
