using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Validators;
using System;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Validators;

public class FeedbackRequestValidatorTests
{
    private readonly FeedbackRequestValidator _sut;

    public FeedbackRequestValidatorTests()
    {
        _sut = new FeedbackRequestValidator();
    }

    [Fact]
    public void Call_IsValidAdd_ReturnsBoolean()
    {
        var request = new FeedbackAddRequest { ImproveService = "TestValue1", OverallRating = "TestValue2" };
        var result = _sut.IsValidAdd(request);
        Assert.True(result);
    }

    [Fact]
    public void Call_IsValidAddWithNullFeedbackAddRequest_ThrowsNullReferenceException()
    {
        Assert.Throws<NullReferenceException>(() => _sut.IsValidAdd(default(FeedbackAddRequest)));
    }
}
