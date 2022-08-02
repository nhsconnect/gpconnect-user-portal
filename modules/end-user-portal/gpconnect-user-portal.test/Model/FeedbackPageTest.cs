using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.Config;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Feedback;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Test.Pages.Feedback;

public class FeedbackPageTest
{
    private readonly Mock<IOptions<ApplicationParameters>> _mockOptions;
    private readonly Mock<IFeedbackService> _mockService;

    public FeedbackPageTest()
    {
        _mockOptions = new Mock<IOptions<ApplicationParameters>>();
        _mockService = new Mock<IFeedbackService>();
    }

    [Fact]
    public void OnGet_ClearsOverallRatingValidation_LeavesUserInput_ReturnsPage()
    {
        var validationKeyOverallRating = "OverallRating";
        var validationKeyImproveService = "ImproveService";
        var expectedOverallRating = "Very satisfied";
        var expectedImproveService = "Excellent";

        var feedbackModel = new FeedbackModel(_mockOptions.Object, _mockService.Object)
        {
            OverallRating = expectedOverallRating,
            ImproveService = expectedImproveService
        };

        feedbackModel.ModelState.AddModelError(validationKeyOverallRating, "My Error 1");
        feedbackModel.ModelState.AddModelError(validationKeyImproveService, "My Error 2");
        
        var result = feedbackModel.OnGet();
        
        Assert.StrictEqual(ModelValidationState.Unvalidated, feedbackModel.ModelState.GetFieldValidationState(validationKeyOverallRating));
        Assert.StrictEqual(ModelValidationState.Unvalidated, feedbackModel.ModelState.GetFieldValidationState(validationKeyImproveService));
        Assert.Equal(expectedOverallRating, feedbackModel.OverallRating);
        Assert.Equal(expectedImproveService, feedbackModel.ImproveService);
        Assert.IsType<PageResult>(result);
    }

    [Theory]
    [InlineData("Very satisfied", "Excellent")]
    [InlineData("Neither satisfied nor disatissfied", "Average")]
    public async void OnPost_IfValidInputParameters_RedirectsToThankyouPage(string overallRating, string improveService)
    {
        var feedbackModel = new FeedbackModel(_mockOptions.Object, _mockService.Object)
        {
            OverallRating = overallRating,
            ImproveService = improveService
        };

        var result = await feedbackModel.OnPost();        
        Assert.IsType<RedirectToPageResult>(result);
    }

    [Theory]
    [InlineData("", "")]
    [InlineData(" ", " ")]
    public async void OnPost_IfInvalidModel_ReturnValidationError(string overallRating, string improveService)
    {
        var feedbackModel = new FeedbackModel(_mockOptions.Object, _mockService.Object)
        {
            OverallRating = overallRating,
            ImproveService = improveService
        };

        feedbackModel.ModelState.AddModelError("OverallRating", "You must select a value for Overall Rating");
        feedbackModel.ModelState.AddModelError("ImproveService", "You must enter a value for Improve Service");

        var result = await feedbackModel.OnPost();
        Assert.IsType<PageResult>(result);
        Assert.True(feedbackModel.ModelState.ErrorCount > 0);
    }
}
