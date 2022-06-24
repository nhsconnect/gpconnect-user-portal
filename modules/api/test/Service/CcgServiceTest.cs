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

public class CcgServiceTest
{
    private readonly Mock<IDataService> _mockDataService;
    private readonly CcgService _sut;

    public CcgServiceTest()
    {
        _mockDataService = new Mock<IDataService>();
        _sut = new CcgService(_mockDataService.Object);
    }

    [Fact]
    public void CanConstruct()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void CannotConstructWithNullDataService()
    {
        Assert.Throws<ArgumentNullException>(() => new CareSettingService(default(IDataService)));
    }

    [Fact]
    public async Task CanCallGetCcgs()
    {
        var list = Task.FromResult(new List<Ccg>());
        _mockDataService.Setup(d => d.ExecuteQuery<Ccg>(It.IsAny<string>(), It.IsAny<DynamicParameters>())).Returns(list);

        var result = await _sut.GetCcgs();
        Assert.IsAssignableFrom<IEnumerable<Ccg>>(result);
    }
}
