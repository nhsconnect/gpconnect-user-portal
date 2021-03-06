using Dapper;
using GpConnect.NationalDataSharingPortal.Api.Dal.Interfaces;
using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using GpConnect.NationalDataSharingPortal.Api.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Service;

public class CareSettingServiceTest
{
    private readonly Mock<IDataService> _mockDataService;
    private readonly CareSettingService _sut;

    public CareSettingServiceTest()
    {
        _mockDataService = new Mock<IDataService>();
        _sut = new CareSettingService(_mockDataService.Object);
    }

    [Fact]
    public void CallConstructor_WithExpectedParameters_ReturnsNotNull()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void Constructor_WithNullDataService_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new CareSettingService(default(IDataService)));
    }

    [Fact]
    public async Task Get_CareSettings_ReturnsListOfCareSettings()
    {
        var list = Task.FromResult(new List<CareSetting>());
        _mockDataService.Setup(d => d.ExecuteQuery<CareSetting>(It.IsAny<string>(), It.IsAny<DynamicParameters>())).Returns(list);

        var result = await _sut.GetCareSettings();
        Assert.IsAssignableFrom<IEnumerable<CareSetting>>(result);
    }

    [Fact]
    public async Task Get_CareSetting_ReturnsCareSetting()
    {
        var single = Task.FromResult(new CareSetting());
        _mockDataService.Setup(d => d.ExecuteQueryFirstOrDefault<CareSetting>(It.IsAny<string>(), It.IsAny<DynamicParameters>())).Returns(single);

        var id = 1;
        var result = await _sut.GetCareSetting(id);
        Assert.IsAssignableFrom<CareSetting>(result);
    }

    [Fact]
    public async Task Call_CareSettingAddRequest_ReturnsCareSetting()
    {
        var single = Task.FromResult(new CareSetting());
        _mockDataService.Setup(d => d.ExecuteQueryFirstOrDefault<CareSetting>(It.IsAny<string>(), It.IsAny<DynamicParameters>())).Returns(single);

        var careSettingAddRequest = new CareSettingAddRequest { CareSettingValue = "TestValue1" };
        var result = await _sut.AddCareSetting(careSettingAddRequest);
        Assert.IsAssignableFrom<CareSetting>(result);
    }

    [Fact]
    public async Task Call_WithNullCareSettingAddRequest_ThrowsNullReferenceException()
    {
        await Assert.ThrowsAsync<NullReferenceException>(() => _sut.AddCareSetting(default(CareSettingAddRequest)));
    }

    [Fact]
    public async Task Call_WithNullCareSettingDisableRequest_ThrowsNullReferenceException()
    {
        await Assert.ThrowsAsync<NullReferenceException>(() => _sut.DisableCareSetting(default(CareSettingDisableRequest)));
    }

    [Fact]
    public async Task Call_WithNullCareSettingUpdateRequest_ThrowsNullReferenceException()
    {
        await Assert.ThrowsAsync<NullReferenceException>(() => _sut.UpdateCareSetting(default(CareSettingUpdateRequest)));
    }
}
