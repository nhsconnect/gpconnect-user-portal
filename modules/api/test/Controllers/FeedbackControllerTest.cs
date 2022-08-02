using GpConnect.NationalDataSharingPortal.Api.Controllers;
using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Service.Interface;
using GpConnect.NationalDataSharingPortal.Api.Validators.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Controllers;

public class FeedbackControllerTest
{
    private readonly FeedbackController _sut;
    private readonly Mock<IFeedbackRequestValidator> _mockValidator;
    private readonly Mock<IFeedbackService> _mockService;
    private readonly Mock<ILogger<FeedbackController>> _mockLogger;

    public FeedbackControllerTest()
    {
        _mockValidator = new Mock<IFeedbackRequestValidator>();
        _mockService = new Mock<IFeedbackService>();
        _mockLogger = new Mock<ILogger<FeedbackController>>();

        _sut = new FeedbackController(_mockValidator.Object, _mockService.Object, _mockLogger.Object);
    }

    [Fact]
    public void CallConstructor_WithExpectedParameters_ReturnsNotNull()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void Constructor_WithNullValidator_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new FeedbackController(default(IFeedbackRequestValidator), _mockService.Object, _mockLogger.Object));
    }

    [Fact]
    public void Constructor_WithNullService_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new FeedbackController(_mockValidator.Object, default(IFeedbackService), _mockLogger.Object));
    }

    [Fact]
    public void Constructor_WithNullLogger_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new FeedbackController(_mockValidator.Object, _mockService.Object, default(ILogger<FeedbackController>)));
    }

    [Fact]
    public async Task Post_WithValidParameters_ReturnsStatusCodeResult()
    {
        var feedbackAddRequest = new FeedbackAddRequest { OverallRating = "TestValue1", ImproveService = "TestValue2" };
        _mockValidator.Setup(v => v.IsValidAdd(It.IsAny<FeedbackAddRequest>())).Returns(true);
        var result = await _sut.CreateFeedback(feedbackAddRequest);
        Assert.IsType<StatusCodeResult>(result);
    }

    [Fact]
    public async Task CreateFeedbackRecord_CallsService_WithExpectedParameters()
    {
        var expected = new FeedbackAddRequest();
        _mockValidator.Setup(v => v.IsValidAdd(It.IsAny<FeedbackAddRequest>())).Returns(true);
        await _sut.CreateFeedback(expected);
        _mockService.Verify(v => v.CreateFeedbackAsync(expected), Times.Once);
    }

    [Fact]
    public async Task Add_WhenValidatorReturnsFalse_ReturnsBadRequest()
    {
        _mockValidator.Setup(v => v.IsValidAdd(It.IsAny<FeedbackAddRequest>())).Returns(false);
        var response = await _sut.CreateFeedback(new FeedbackAddRequest());
        Assert.IsType<BadRequestResult>(response);
    }

    [Fact]
    public async Task Add_WhenValidatorReturnsTrue_WithExpectedParameters()
    {
        _mockValidator.Setup(v => v.IsValidAdd(It.IsAny<FeedbackAddRequest>())).Returns(true);
        var response = await _sut.CreateFeedback(new FeedbackAddRequest());
        Assert.IsType<StatusCodeResult>(response);
    }

    [Fact]
    public async Task Add_WhenServiceThrows_ThrowsException()
    {
        _mockValidator.Setup(v => v.IsValidAdd(It.IsAny<FeedbackAddRequest>())).Returns(true);
        _mockService.Setup(s => s.CreateFeedbackAsync(It.IsAny<FeedbackAddRequest>())).Throws(new Exception(DateTime.Now.ToString()));
        await Assert.ThrowsAsync<Exception>(() => _sut.CreateFeedback(new FeedbackAddRequest()));
    }
}
