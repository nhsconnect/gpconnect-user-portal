using Dapper;
using GpConnect.NationalDataSharingPortal.Api.Dal.Interfaces;
using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using GpConnect.NationalDataSharingPortal.Api.Service;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Service;

public class FeedbackServiceTest
{
    private readonly Mock<IDataService> _mockDataService;
    private readonly FeedbackService _sut;

    public FeedbackServiceTest()
    {
        _mockDataService = new Mock<IDataService>();
        _sut = new FeedbackService(_mockDataService.Object);
    }

    [Fact]
    public void CallConstructor_WithExpectedParameters_ReturnsNotNull()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public async Task Call_SupplierAddRequest_ReturnsSupplier()
    {
        var single = Task.FromResult(new Supplier());
        _mockDataService.Setup(d => d.ExecuteQueryFirstOrDefault<Supplier>(It.IsAny<string>(), It.IsAny<DynamicParameters>())).Returns(single);

        var feedbackAddRequest = new FeedbackAddRequest { OverallRating = "TestValue1", ImproveService = "TestValue2" };
        await _sut.CreateFeedbackAsync(feedbackAddRequest);        
    }

    [Fact]
    public async Task Call_WithNullSupplierAddRequest_ThrowsNullReferenceException()
    {
        await Assert.ThrowsAsync<NullReferenceException>(() => _sut.CreateFeedbackAsync(default(FeedbackAddRequest)));
    }
}
