using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using System;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dto.Response;

public class UserTests
{
    private readonly User _sut;

    public UserTests()
    {
        _sut = new User();
    }

    [Fact]
    public void CallConstructor_WithExpectedParameters_ReturnsNotNull()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void PropertyUserId_WithProvidedValues_CanSetAndGet()
    {
        var testValue = 1;
        _sut.UserId = testValue;
        Assert.Equal(testValue, _sut.UserId);
    }

    [Fact]
    public void PropertyEmailAddress_WithProvidedValues_CanSetAndGet()
    {
        var testValue = "TestValue1";
        _sut.EmailAddress = testValue;
        Assert.Equal(testValue, _sut.EmailAddress);
    }

    [Fact]
    public void PropertyLastLogonDate_WithProvidedValues_CanSetAndGet()
    {
        var testValue = default(DateTime?);
        _sut.LastLogonDate = testValue;
        Assert.Equal(testValue, _sut.LastLogonDate);
    }

    [Fact]
    public void PropertyIsAdmin_WithProvidedValues_CanSetAndGet()
    {
        var testValue = false;
        _sut.IsAdmin = testValue;
        Assert.Equal(testValue, _sut.IsAdmin);
    }
}
