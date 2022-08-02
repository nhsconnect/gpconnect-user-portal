using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Feedback;

public partial class FeedbackModel : BaseModel
{
    private readonly IFeedbackService _feedbackService;

    public FeedbackModel(IOptions<ApplicationParameters> applicationParameters, IFeedbackService feedbackService) : base(applicationParameters)
    {
        _feedbackService = feedbackService;
    }

    public IActionResult OnGet()
    {
        ClearModelState();
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        await _feedbackService.SubmitFeedbackAsync(OverallRating, ImproveService);
        return RedirectToPage("./Thankyou");
    }

    private void ClearModelState()
    {
        ModelState.ClearValidationState("OverallRating");
        ModelState.ClearValidationState("ImproveService");
    }
}
