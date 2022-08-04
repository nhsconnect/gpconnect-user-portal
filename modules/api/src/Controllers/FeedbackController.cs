using System;
using System.Threading.Tasks;
using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Service.Interface;
using GpConnect.NationalDataSharingPortal.Api.Validators.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GpConnect.NationalDataSharingPortal.Api.Controllers;

[ApiController]
[Route("feedback")]
public class FeedbackController : ControllerBase
{
    private readonly IFeedbackService _feedbackService;
    private readonly IFeedbackRequestValidator _validator;
    private readonly ILogger<FeedbackController> _logger;

    public FeedbackController(IFeedbackRequestValidator feedbackRequestValidator, IFeedbackService feedbackService, ILogger<FeedbackController> logger)
    {
        _feedbackService = feedbackService ?? throw new ArgumentNullException();
        _validator = feedbackRequestValidator ?? throw new ArgumentNullException();
        _logger = logger ?? throw new ArgumentNullException();
    }

    [HttpPost]
    public async Task<IActionResult> CreateFeedback([FromBody] FeedbackAddRequest feedbackAddRequest)
    {
        if (!_validator.IsValidAdd(feedbackAddRequest))
        {
            _logger.LogWarning("Invalid Request");
            return BadRequest();
        }

        await _feedbackService.CreateFeedbackAsync(feedbackAddRequest);
        
        return StatusCode(201);
    }
}
