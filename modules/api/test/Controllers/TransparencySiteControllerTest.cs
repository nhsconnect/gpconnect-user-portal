using System;
using System.Collections.Generic;
using System.Linq;
using gpconnect_user_portal.api.Controllers;
using gpconnect_user_portal.api.dto;
using gpconnect_user_portal.api.dto.request;
using gpconnect_user_portal.api.service;
using gpconnect_user_portal.api.validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace gpconnect_user_portal.api.test.controllers;

public class TransparencySiteControllerTest
{
    private readonly TransparencySiteController _sut;
    private readonly Mock<ITransparencySiteRequestValidator> _mockValidator;
    private readonly Mock<ITransparencySiteService> _mockService;

    // setup
    public TransparencySiteControllerTest()
    {
        _mockValidator = new Mock<ITransparencySiteRequestValidator>();
        _mockService = new Mock<ITransparencySiteService>(); 

        _sut = new TransparencySiteController(_mockValidator.Object, _mockService.Object, (new Mock<ILogger<TransparencySiteController>>()).Object);
    }

    [Fact]
    public void Get_CallsValidator_WithExpectedParameters()
    {
        var expected = new TransparencySiteRequest();

        _sut.Get(expected);

        _mockValidator.Verify(v => v.IsValid(expected), Times.Once);
    }

    [Fact]
    public void Get_WhenValidatorReturnsFalse_ReturnsBadRequest()
    {
        _mockValidator.Setup(v => v.IsValid(It.IsAny<TransparencySiteRequest>())).Returns(false);

        var response = _sut.Get(new TransparencySiteRequest());

        Assert.IsType<BadRequestResult>(response.Result);
    }

    [Fact]
    public void Get_WhenValidatorReturnsTrue_CallsService_WithExpectedParameters()
    {
        var expected = new TransparencySiteRequest();

        _mockValidator.Setup(v => v.IsValid(It.IsAny<TransparencySiteRequest>())).Returns(true);

        var response = _sut.Get(expected);

       _mockService.Verify(s => s.GetMatchingSites(expected), Times.Once);
    }

    [Fact]
    public void Get_WhenServiceThrows_ThrowsException()
    {
        _mockValidator.Setup(v => v.IsValid(It.IsAny<TransparencySiteRequest>())).Returns(true);
        _mockService.Setup(s => s.GetMatchingSites(It.IsAny<TransparencySiteRequest>())).Throws(new System.Exception("Boom!!!"));

        Assert.Throws<Exception>(() => _sut.Get(new TransparencySiteRequest()));
    }

    [Fact]
    public void Get_WhenServiceReturns_ReturnsOkRequestWithArray()
    {
        _mockValidator.Setup(v => v.IsValid(It.IsAny<TransparencySiteRequest>())).Returns(true);
        _mockService.Setup(s => s.GetMatchingSites(It.IsAny<TransparencySiteRequest>())).Returns(
            new List<TransparencySite>
            { 
                new TransparencySite { Name = "Test"}
            }
        );

        var response = _sut.Get(new TransparencySiteRequest());

        Assert.NotNull(response.Result);
        Assert.IsType<OkObjectResult>(response.Result);

        var result = response.Result as OkObjectResult;
        var value = result?.Value as IEnumerable<TransparencySite>;

        Assert.StrictEqual(1, value?.ToList().Count);
        Assert.StrictEqual("Test", value?.ToList()[0].Name);
    }
}
