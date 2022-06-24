using Dapper;
using GpConnect.NationalDataSharingPortal.Api.Dal.Interfaces;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using GpConnect.NationalDataSharingPortal.Api.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Service;

public class UserServiceTest
{
    private readonly Mock<IDataService> _mockDataService;
    private readonly UserService _sut;

    public UserServiceTest()
    {
        _mockDataService = new Mock<IDataService>();
        _sut = new UserService(_mockDataService.Object);
    }

    [Fact]
    public void CanConstruct()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void CannotConstructWithNullDataService()
    {
        Assert.Throws<ArgumentNullException>(() => new UserService(default(IDataService)));
    }

    [Fact]
    public async Task CanCallGetSuppliers()
    {
        var list = Task.FromResult(new List<User>());
        _mockDataService.Setup(d => d.ExecuteQuery<User>(It.IsAny<string>(), It.IsAny<DynamicParameters>())).Returns(list);

        var result = await _sut.GetUsers();
        Assert.IsAssignableFrom<IEnumerable<User>>(result);
    }
}
