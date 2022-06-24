using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using System;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dto.Response;

public class TransparencySiteTests
{
    private readonly TransparencySite _sut;

    public TransparencySiteTests()
    {
        _sut = new TransparencySite();
    }

    [Fact]
    public void CanConstruct()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void CanSetAndGetId()
    {
        var testValue = default(Guid);
        _sut.Id = testValue;
        Assert.Equal(testValue, _sut.Id);
    }

    [Fact]
    public void CanSetAndGetOdsCode()
    {
        var testValue = "TestValue1";
        _sut.OdsCode = testValue;
        Assert.Equal(testValue, _sut.OdsCode);
    }

    [Fact]
    public void CanSetAndGetName()
    {
        var testValue = "TestValue1";
        _sut.Name = testValue;
        Assert.Equal(testValue, _sut.Name);
    }

    [Fact]
    public void CanSetAndGetAddressLine1()
    {
        var testValue = "TestValue1";
        _sut.AddressLine1 = testValue;
        Assert.Equal(testValue, _sut.AddressLine1);
    }

    [Fact]
    public void CanSetAndGetAddressLine2()
    {
        var testValue = "TestValue1";
        _sut.AddressLine2 = testValue;
        Assert.Equal(testValue, _sut.AddressLine2);
    }

    [Fact]
    public void CanSetAndGetTown()
    {
        var testValue = "TestValue1";
        _sut.Town = testValue;
        Assert.Equal(testValue, _sut.Town);
    }

    [Fact]
    public void CanSetAndGetCounty()
    {
        var testValue = "TestValue1";
        _sut.County = testValue;
        Assert.Equal(testValue, _sut.County);
    }

    [Fact]
    public void CanSetAndGetCountry()
    {
        var testValue = "TestValue1";
        _sut.Country = testValue;
        Assert.Equal(testValue, _sut.Country);
    }

    [Fact]
    public void CanSetAndGetPostcode()
    {
        var testValue = "TestValue1";
        _sut.Postcode = testValue;
        Assert.Equal(testValue, _sut.Postcode);
    }

    [Fact]
    public void CanSetAndGetAccessRecordHTMLEnabled()
    {
        var testValue = true;
        _sut.AccessRecordHTMLEnabled = testValue;
        Assert.Equal(testValue, _sut.AccessRecordHTMLEnabled);
    }

    [Fact]
    public void CanSetAndGetStructuredRecordEnabled()
    {
        var testValue = false;
        _sut.StructuredRecordEnabled = testValue;
        Assert.Equal(testValue, _sut.StructuredRecordEnabled);
    }

    [Fact]
    public void CanSetAndGetAppointmentManagementEnabled()
    {
        var testValue = false;
        _sut.AppointmentManagementEnabled = testValue;
        Assert.Equal(testValue, _sut.AppointmentManagementEnabled);
    }

    [Fact]
    public void CanSetAndGetSendDocumentEnabled()
    {
        var testValue = false;
        _sut.SendDocumentEnabled = testValue;
        Assert.Equal(testValue, _sut.SendDocumentEnabled);
    }

    [Fact]
    public void CanSetAndGetCcgIcbName()
    {
        var testValue = "TestValue1";
        _sut.CcgIcbName = testValue;
        Assert.Equal(testValue, _sut.CcgIcbName);
    }

    [Fact]
    public void CanSetAndGetCcgIcbOdsCode()
    {
        var testValue = "TestValue1";
        _sut.CcgIcbOdsCode = testValue;
        Assert.Equal(testValue, _sut.CcgIcbOdsCode);
    }

    [Fact]
    public void CanSetAndGetUseCase()
    {
        var testValue = "TestValue1";
        _sut.UseCase = testValue;
        Assert.Equal(testValue, _sut.UseCase);
    }
}
