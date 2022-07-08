using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Service.Interface;
using GpConnect.NationalDataSharingPortal.Api.Validators;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Validators;

public class CareSettingRequestValidatorTests
{
    private readonly Mock<ICareSettingService> _mockService;
    private readonly CareSettingRequestValidator _sut;

    public CareSettingRequestValidatorTests()
    {
        _mockService = new Mock<ICareSettingService>();
        _sut = new CareSettingRequestValidator(_mockService.Object);
    }

    [Fact]
    public void CallConstructor_WithExpectedParameters_ReturnsNotNull()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void Constructor_WithNullCareSettingService_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new CareSettingRequestValidator(default(ICareSettingService)));
    }

    [Fact]
    public async Task Call_IsValidUpdate_ReturnsBoolean()
    {
        var request = new CareSettingUpdateRequest { CareSettingId = 1, CareSettingValue = "TestValue1" };
        var result = await _sut.IsValidUpdate(request);
        Assert.True(result.RequestValid);
    }

    [Fact]
    public async Task Call_IsValidUpdateWithNullCareSettingUpdateRequest_ThrowsNullReferenceException()
    {
        await Assert.ThrowsAsync<NullReferenceException>(() => _sut.IsValidUpdate(default(CareSettingUpdateRequest)));
    }

    [Fact]
    public async Task Call_IsValidDisable_ReturnsBoolean()
    {
        var request = new CareSettingDisableRequest { CareSettingId = 1, CareSettingDisabled = false };
        var result = await _sut.IsValidDisable(request);
        Assert.True(result.RequestValid);
    }

    [Fact]
    public async Task Call_IsValidDisableWithNullCareSettingDisableRequest_ThrowsNullReferenceException()
    {
        await Assert.ThrowsAsync<NullReferenceException>(() => _sut.IsValidDisable(default(CareSettingDisableRequest)));
    }

    [Fact]
    public void Call_IsValidAdd_ReturnsBoolean()
    {
        var request = new CareSettingAddRequest { CareSettingValue = "TestValue1" };
        var result = _sut.IsValidAdd(request);
        Assert.True(result);
    }

    [Fact]
    public void Call_IsValidAddWithNullCareSettingAddRequest_ThrowsNullReferenceException()
    {
        Assert.Throws<NullReferenceException>(() => _sut.IsValidAdd(default(CareSettingAddRequest)));
    }
}
