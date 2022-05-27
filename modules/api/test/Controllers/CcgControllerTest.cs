using GpConnect.NationalDataSharingPortal.Api.Controllers;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using GpConnect.NationalDataSharingPortal.Api.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Controllers;

public class CcgControllerTest
{
    private readonly CcgController _sut;
    private readonly Mock<ICcgService> _mockService;
    private readonly Mock<ILogger<CcgController>> _mockLogger;

    // setup
    public CcgControllerTest()
    {
        _mockService = new Mock<ICcgService>();
        _mockLogger = new Mock<ILogger<CcgController>>();
        _sut = new CcgController(_mockService.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task Get_WhenServiceReturns_ReturnsOkResponseAsync()
    {
        _mockService.Setup(x => x.GetCcgs()).ReturnsAsync(new List<Ccg>
            {
                new Ccg { CcgId = 1, CcgLinkedId = 2, CcgName = "CCG 1", CcgOdsCode = "A12345"},
                new Ccg { CcgId = 3, CcgLinkedId = 4, CcgName = "CCG 2", CcgOdsCode = "A98765"}
            });           

        var response = await _sut.Get();

        Assert.NotNull(response.Result);
        Assert.IsType<OkObjectResult>(response.Result);

        var result = response.Result as OkObjectResult;
        var value = result?.Value as IEnumerable<Ccg>;

        Assert.StrictEqual(2, value?.ToList().Count);
        Assert.Equal("CCG 1", value?.ToList()[0].CcgName);
    }
}