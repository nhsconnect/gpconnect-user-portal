using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using GpConnect.NationalDataSharingPortal.Api.Dal.Interfaces;
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
    public void GetMatchingSitesAsync_WithNameQuery_CallsDataService_WithExpectedParameters()
    {
        _sut.GetMatchingSitesAsync(new TransparencySiteRequest
        {
            ProviderName = "Name"
        });

        _mockDataService.Verify(m => m.ExecuteQuery<TransparencySite>(
            "application.find_sites",
            It.Is<DynamicParameters>(d => d.ParameterNames.AsList<string>().Contains("_site_name") 
                                        && d.Get<string>("_site_name") == "Name"
                                        && d.Get<int>("_site_definition_status_min") == 5
                                        && d.Get<int>("_site_definition_status_max") == 5
                                    )
        ));        
    }

    [Fact]
    public void GetMatchingSitesAsync_WithODSQuery_CallsDataService_WithExpectedParameters()
    {
        _sut.GetMatchingSitesAsync(new TransparencySiteRequest
        {
            ProviderCode = "Code"
        });

         _mockDataService.Verify(m => m.ExecuteQuery<TransparencySite>(
            "application.find_sites",
            It.Is<DynamicParameters>(d => d.ParameterNames.AsList<string>().Contains("_site_ods_code") 
                                        && d.Get<string>("_site_ods_code") == "Code"
                                        && d.Get<int>("_site_definition_status_min") == 5
                                        && d.Get<int>("_site_definition_status_max") == 5
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
    public void GetMatchingSitesAsync_ExecuteQuerySucceeds_Returns()
    {
        var expected = Task.FromResult<List<TransparencySite>>(new List<TransparencySite>());

        _mockDataService.Setup(d => d.ExecuteQuery<TransparencySite>(It.IsAny<string>(), It.IsAny<DynamicParameters>())).Returns(expected);

        var result = _sut.GetMatchingSitesAsync(new TransparencySiteRequest());

        Assert.StrictEqual(expected, result);
    }
}