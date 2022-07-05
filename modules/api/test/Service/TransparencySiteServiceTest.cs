using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using GpConnect.NationalDataSharingPortal.Api.Dal.Interfaces;
using GpConnect.NationalDataSharingPortal.Api.Dal.Mapping;
using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using GpConnect.NationalDataSharingPortal.Api.Service;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Service;

public class TransparencySiteServiceTest
{
    private readonly Mock<IDataService> _mockDataService;
    private readonly Mock<ILogger<TransparencySiteService>> _mockLogger;
    private readonly TransparencySiteService _sut;

    public TransparencySiteServiceTest()
    {
        _mockDataService = new Mock<IDataService>();
        _mockLogger = new Mock<ILogger<TransparencySiteService>>();

        _sut = new TransparencySiteService(_mockDataService.Object, _mockLogger.Object);
    }

    [Fact]
    public void CallConstructor_WithExpectedParameters_ReturnsNotNull()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void Constructor_WithNullDataService_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new TransparencySiteService(default(IDataService), _mockLogger.Object));
    }

    [Fact]
    public void Constructor_WithNullLogger_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new TransparencySiteService(_mockDataService.Object, default(ILogger<TransparencySiteService>)));
    }

    [Fact]
    public async Task Get_GetMatchingSites_ReturnsListOfSites()
    {
        var request = new TransparencySiteRequest { ProviderCode = default(string?), ProviderName = default(string?), StartPosition = 1, Count = 10 };
        var result = await _sut.GetMatchingSitesAsync(request);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task Call_WithNullTransparencySiteRequest_ThrowsNullReferenceException()
    {
        await Assert.ThrowsAsync<NullReferenceException>(() => _sut.GetMatchingSitesAsync(default(TransparencySiteRequest)));
    }

    [Fact]
    public void Get_Site_ReturnsSite()
    {
        var id = default(Guid);
        var result = _sut.GetSiteAsync(id);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetMatchingSitesAsync_WithNameQuery_CallsDataService_WithExpectedParameters()
    {
        await _sut.GetMatchingSitesAsync(new TransparencySiteRequest
        {
            ProviderName = "Name"
        });

        _mockDataService.Verify(m => m.ExecuteScalar(
            "application.find_sites_count",
            It.Is<DynamicParameters>(d => d.ParameterNames.AsList<string>().Contains("_site_name")
                                        && d.Get<string>("_site_name") == "Name"
                                    )
        ));

        _mockDataService.Verify(m => m.ExecuteQuery<TransparencySite>(
            "application.find_sites",
            It.Is<DynamicParameters>(d => d.ParameterNames.AsList<string>().Contains("_site_name")
                                        && d.Get<string>("_site_name") == "Name"
                                        && d.Get<int>("_start_position") == 1
                                        && d.Get<int>("_number_to_return") == int.MaxValue
                                    )
        ));
    }

    [Fact]
    public async Task GetMatchingSitesAsync_WithODSQuery_CallsDataService_WithExpectedParameters()
    {
        await _sut.GetMatchingSitesAsync(new TransparencySiteRequest
        {
            ProviderCode = "Code"
        });

        _mockDataService.Verify(m => m.ExecuteScalar(
            "application.find_sites_count",
            It.Is<DynamicParameters>(d => d.ParameterNames.AsList<string>().Contains("_site_ods_code")
                                        && d.Get<string>("_site_ods_code") == "Code"
                                    )
        ));

        _mockDataService.Verify(m => m.ExecuteQuery<TransparencySite>(
           "application.find_sites",
           It.Is<DynamicParameters>(d => d.ParameterNames.AsList<string>().Contains("_site_ods_code")
                                       && d.Get<string>("_site_ods_code") == "Code"
                                      && d.Get<int>("_start_position") == 1
                                        && d.Get<int>("_number_to_return") == int.MaxValue
                                   )
       ));
    }

    [Fact]
    public void GetMatchingSitesAsync_ExecuteQueryThrows_Throws()
    {
        _mockDataService.Setup(d => d.ExecuteQuery<TransparencySite>(It.IsAny<string>(), It.IsAny<DynamicParameters>())).Throws(new Exception());

        Assert.ThrowsAsync<Exception>(async () => await _sut.GetMatchingSitesAsync(new TransparencySiteRequest()));
    }

    [Fact]
    public void GetMatchingSitesCountAsync_ExecuteQueryThrows_Throws()
    {
        _mockDataService.Setup(d => d.ExecuteScalar(It.IsAny<string>(), It.IsAny<DynamicParameters>())).Throws(new Exception());
        Assert.ThrowsAsync<Exception>(async () => await _sut.GetMatchingSitesAsync(new TransparencySiteRequest()));
    }    

    [Fact]
    public async Task GetMatchingSitesAsync_ExecuteQuerySucceeds_Returns()
    {
        var expected = new TransparencySites() { TotalResults = 1, Results = new List<TransparencySite>()};
        var list = Task.FromResult(new List<TransparencySite>());

        _mockDataService.Setup(d => d.ExecuteQuery<TransparencySite>(It.IsAny<string>(), It.IsAny<DynamicParameters>())).Returns(list);

        var sites = await _sut.GetMatchingSitesAsync(new TransparencySiteRequest());

        var result = Task.FromResult(new TransparencySites() { Results = sites.Results, TotalResults = 1 });
        
        Assert.Equal(expected.Results, result.Result.Results);
        Assert.Equal(expected.TotalResults, result.Result.TotalResults);
    }

    public async Task GetMatchingSitesCountAsync_ExecuteQuerySucceeds_Returns()
    {
        var expectedResultCount = 1;

        _mockDataService.Setup(d => d.ExecuteScalar(It.IsAny<string>(), It.IsAny<DynamicParameters>())).ReturnsAsync(expectedResultCount);

        var result = await _sut.GetMatchingSitesAsync(new TransparencySiteRequest());

        Assert.StrictEqual(expectedResultCount, result.TotalResults);
    }

    [Fact]
    public void GetSiteAsync_CallsDataService_WithExpectedParameters()
    {
        var expected = new Guid();

        _sut.GetSiteAsync(expected);

        _mockDataService.Verify(m => m.ExecuteQueryFirstOrDefault<TransparencySite>(
           "application.find_site",
           It.Is<DynamicParameters>(d => d.ParameterNames.AsList<string>().Contains("_site_unique_identifier")
                                       && d.Get<Guid>("_site_unique_identifier") == expected
                                   )
       ));
    }

    [Fact]
    public void GetSiteAsync_ExecuteQueryThrows_Throws()
    {
        _mockDataService.Setup(d => d.ExecuteQueryFirstOrDefault<TransparencySite>(It.IsAny<string>(), It.IsAny<DynamicParameters>())).Throws(new Exception());

        Assert.ThrowsAsync<Exception>(async () => await _sut.GetSiteAsync(new Guid()));
    }

    [Fact]
    public void GetSiteAsync_ExecuteQuerySucceeds_Returns()
    {
        var expected = Task.FromResult(new TransparencySite());

        _mockDataService.Setup(d => d.ExecuteQueryFirstOrDefault<TransparencySite>(It.IsAny<string>(), It.IsAny<DynamicParameters>())).Returns(expected);

        var result = _sut.GetSiteAsync(new Guid());

        Assert.StrictEqual(expected, result);
    }

    [Fact]
    public async Task GetSiteAsync_Returns_ExpectedFieldMappings()
    {
        var expected = Task.FromResult(new TransparencySite());
        _mockDataService.Setup(d => d.ExecuteQueryFirstOrDefault<TransparencySite>(It.IsAny<string>(), It.IsAny<DynamicParameters>())).Returns(expected);
        var result = await _sut.GetSiteAsync(new Guid());

        var resultTypes = result.GetType().GetProperties();
        var map = new TransparencySiteMap();
        var propertyMaps = map.PropertyMaps.ToList();
        var propertyCount = resultTypes.Count();

        Assert.Equal(propertyCount, map.PropertyMaps.Count);
        var notMappedElements = propertyMaps.Where(p => resultTypes.All(p2 => p2.Name != p.PropertyInfo.Name));
        Assert.Empty(notMappedElements);

        foreach (var resultType in resultTypes)
        {
            Assert.NotNull(resultType);
            var propertyMap = map.PropertyMaps.Single(x => x.PropertyInfo.Name == resultType.Name);
            Assert.NotNull(propertyMap);
            Assert.Equal(resultType.Name, propertyMap.PropertyInfo.Name);
            Assert.NotNull(propertyMap.ColumnName);
            Assert.True(propertyMap.CaseSensitive);
        }
        Assert.Equal(propertyCount, propertyMaps.Count);
    }
}
