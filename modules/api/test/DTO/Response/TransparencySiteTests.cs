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
    public void CallConstructor_WithExpectedParameters_ReturnsNotNull()
    {
        Assert.NotNull(_sut);
    }

    [Fact]
    public void PropertyId_WithProvidedValues_CanSetAndGet()
    {
        var testValue = default(Guid);
        _sut.Id = testValue;
        Assert.Equal(testValue, _sut.Id);
    }

    [Fact]
    public void PropertyOdsCode_WithProvidedValues_CanSetAndGet()
    {
        var testValue = "TestValue1";
        _sut.OdsCode = testValue;
        Assert.Equal(testValue, _sut.OdsCode);
    }

    [Fact]
    public void PropertyName_WithProvidedValues_CanSetAndGet()
    {
        var testValue = "TestValue1";
        _sut.Name = testValue;
        Assert.Equal(testValue, _sut.Name);
    }

    [Fact]
    public void PropertyAddressLine1_WithProvidedValues_CanSetAndGet()
    {
        var testValue = "TestValue1";
        _sut.AddressLine1 = testValue;
        Assert.Equal(testValue, _sut.AddressLine1);
    }

    [Fact]
    public void PropertyAddressLine2_WithProvidedValues_CanSetAndGet()
    {
        var testValue = "TestValue1";
        _sut.AddressLine2 = testValue;
        Assert.Equal(testValue, _sut.AddressLine2);
    }

    [Fact]
    public void PropertyTown_WithProvidedValues_CanSetAndGet()
    {
        var testValue = "TestValue1";
        _sut.Town = testValue;
        Assert.Equal(testValue, _sut.Town);
    }

    [Fact]
    public void PropertyCounty_WithProvidedValues_CanSetAndGet()
    {
        var testValue = "TestValue1";
        _sut.County = testValue;
        Assert.Equal(testValue, _sut.County);
    }

    [Fact]
    public void PropertyCountry_WithProvidedValues_CanSetAndGet()
    {
        var testValue = "TestValue1";
        _sut.Country = testValue;
        Assert.Equal(testValue, _sut.Country);
    }

    [Fact]
    public void PropertyPostcode_WithProvidedValues_CanSetAndGet()
    {
        var testValue = "TestValue1";
        _sut.Postcode = testValue;
        Assert.Equal(testValue, _sut.Postcode);
    }

    [Fact]
    public void PropertyAccessRecordHTMLEnabled_WithProvidedValues_CanSetAndGet()
    {
        var testValue = true;
        _sut.AccessRecordHTMLEnabled = testValue;
        Assert.Equal(testValue, _sut.AccessRecordHTMLEnabled);
    }

    [Fact]
    public void PropertyStructuredRecordEnabled_WithProvidedValues_CanSetAndGet()
    {
        var testValue = false;
        _sut.StructuredRecordEnabled = testValue;
        Assert.Equal(testValue, _sut.StructuredRecordEnabled);
    }

    [Fact]
    public void PropertyAppointmentManagementEnabled_WithProvidedValues_CanSetAndGet()
    {
        var testValue = false;
        _sut.AppointmentManagementEnabled = testValue;
        Assert.Equal(testValue, _sut.AppointmentManagementEnabled);
    }

    [Fact]
    public void PropertySendDocumentEnabled_WithProvidedValues_CanSetAndGet()
    {
        var testValue = false;
        _sut.SendDocumentEnabled = testValue;
        Assert.Equal(testValue, _sut.SendDocumentEnabled);
    }

    [Fact]
    public void PropertyCcgIcbName_WithProvidedValues_CanSetAndGet()
    {
        var testValue = "TestValue1";
        _sut.CcgIcbName = testValue;
        Assert.Equal(testValue, _sut.CcgIcbName);
    }

    [Fact]
    public void PropertyCcgIcbOdsCode_WithProvidedValues_CanSetAndGet()
    {
        var testValue = "TestValue1";
        _sut.CcgIcbOdsCode = testValue;
        Assert.Equal(testValue, _sut.CcgIcbOdsCode);
    }

    [Fact]
    public void PropertyUseCase_WithProvidedValues_CanSetAndGet()
    {
        var testValue = "TestValue1";
        _sut.UseCase = testValue;
        Assert.Equal(testValue, _sut.UseCase);
    }
}
