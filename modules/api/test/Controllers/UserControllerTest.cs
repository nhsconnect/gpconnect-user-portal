using GpConnect.NationalDataSharingPortal.Api.Controllers;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using GpConnect.NationalDataSharingPortal.Api.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Controllers;

public class UserControllerTest
{
    private readonly UserController _sut;
    private readonly Mock<IUserService> _mockService;
    private readonly Mock<ILogger<UserController>> _mockLogger;

    // setup
    public UserControllerTest()
    {
        _mockService = new Mock<IUserService>();
        _mockLogger = new Mock<ILogger<UserController>>();

         _sut = new UserController(_mockService.Object, _mockLogger.Object);
    }

    [Fact]
    public void CallConstructor_WithExpectedParameters_ReturnsNotNull()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void Constructor_WithNullService_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new UserController(default(IUserService), _mockLogger.Object));
    }

    [Fact]
    public void Constructor_WithNullLogger_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new UserController(_mockService.Object, default(ILogger<UserController>)));
    }

    [Fact]
    public async Task Get_WithNoParameters_ReturnsListOfUsers()
    {
        var result = await _sut.Get();
        Assert.IsType<ActionResult<IEnumerable<User>>>(result);
    }

    [Fact]
    public async Task Get_WithValidParameters_ReturnsOk()
    {
        _mockService.Setup(x => x.GetUsers()).ReturnsAsync(new List<User>
            {
                new User { UserId = 1, EmailAddress = "a@b.com", IsAdmin = false, LastLogonDate = DateTime.Today },
                new User { UserId = 2, EmailAddress = "c@d.com", IsAdmin = true, LastLogonDate = DateTime.Today }
            });

        var response = await _sut.Get();

        Assert.NotNull(response.Result);
        Assert.IsType<OkObjectResult>(response.Result);

        var result = response.Result as OkObjectResult;
        var value = result?.Value as IEnumerable<User>;

        Assert.StrictEqual(2, value?.ToList().Count);
        Assert.Equal("a@b.com", value?.ToList()[0].EmailAddress);
    }
}
