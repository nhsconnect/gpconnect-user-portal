using System;
using System.Collections.Generic;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Builders;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Builders.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Request;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Response;
using Moq;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Test.Builders;

public class AgreementInformationBuilderTest
{
    private readonly Mock<IOrganisationBuilder> _mockOrganisationBuilder;
    private readonly Mock<IInteractionsBuilder> _mockInteractionsBuilder;
    private readonly Mock<ISignatoryBuilder> _mockSignatoryBuilder;
    
    private readonly AgreementInformationBuilder _sut;

    public AgreementInformationBuilderTest()
    {
        _mockOrganisationBuilder = new Mock<IOrganisationBuilder>();
        _mockInteractionsBuilder = new Mock<IInteractionsBuilder>();
        _mockSignatoryBuilder = new Mock<ISignatoryBuilder>();

        _sut = new AgreementInformationBuilder(_mockOrganisationBuilder.Object, _mockInteractionsBuilder.Object, _mockSignatoryBuilder.Object);
    }

    [Fact]
    public void Build_CallsOrganisationBuilder_WithExpectedParameters()
    {
        var expectedOrganisation = new OrganisationResult();

        _sut.Build(expectedOrganisation, new SoftwareSupplierResult(), null, null, null, null, null);

        _mockOrganisationBuilder.Verify(mob => mob.Build(expectedOrganisation), Times.Once);
    }

    [Fact]
    public void Build_CallsSignatoryBuilder_WithExpectedParameters()
    {
        var expectedName = "Name";
        var expectedEmail = "Email";
        var expectedPosition = "Position";

        _sut.Build(null, new SoftwareSupplierResult(), null, null, expectedName, expectedEmail, expectedPosition);

        _mockSignatoryBuilder.Verify(msb => msb.Build(expectedName, expectedEmail, expectedPosition), Times.Once);
    }

    [Fact]
    public void Build_CallsInteractionsBuilder_WithExpectedParameters()
    {
        var expectedInteractions = new List<GpConnectInteractionForSupplier>();

        _sut.Build(null, new SoftwareSupplierResult(), null, expectedInteractions, null, null, null);

        _mockInteractionsBuilder.Verify(mib => mib.Build(expectedInteractions), Times.Once);
    }

    [Fact]
    public void Build_OrganisationBuilderThrows_Throws()
    {
        _mockOrganisationBuilder.Setup(mob => mob.Build(It.IsAny<OrganisationResult>())).Throws(new Exception("Boom!!!"));

        Assert.Throws<Exception>(() => _sut.Build(null, new SoftwareSupplierResult(), null, null, null, null, null));   
    }

    [Fact]
    public void Build_SignatoryBuilderThrows_Throws()
    {
        _mockSignatoryBuilder.Setup(msb => msb.Build(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception("Boom!!!"));

        Assert.Throws<Exception>(() => _sut.Build(null, new SoftwareSupplierResult(), null, null, null, null, null));   
    }


    [Fact]
    public void Build_InteractionsBuilderThrows_Throws()
    {
        _mockInteractionsBuilder.Setup(mib => mib.Build(It.IsAny<List<GpConnectInteractionForSupplier>>())).Throws(new Exception("Boom!!!"));

        Assert.Throws<Exception>(() => _sut.Build(null, new SoftwareSupplierResult(), null, null, null, null, null));   
    }

    [Fact]
    public void Build_GivenBuildersReturn_ReturnsExpectedObject()
    {
        var expectedInteractions = new GpConnectInteractions();
        var expectedSupplierName = "Supplier";
        var expectedUseCase = "UseCase";
        var expectedOrganisation = new OrganisationInformation();
        var expectedSignatory = new SignatoryDetails();

        _mockInteractionsBuilder.Setup(mib => mib.Build(It.IsAny<List<GpConnectInteractionForSupplier>>())).Returns(expectedInteractions);
        _mockOrganisationBuilder.Setup(mob => mob.Build(It.IsAny<OrganisationResult>())).Returns(expectedOrganisation);
        _mockSignatoryBuilder.Setup(mib => mib.Build(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(expectedSignatory);


        var result = _sut.Build(null, new SoftwareSupplierResult {
            SoftwareSupplierName = expectedSupplierName
        }, expectedUseCase, null, null, null, null);

        Assert.StrictEqual(expectedOrganisation, result.Organisation);
        Assert.StrictEqual(expectedSupplierName, result.SoftwareSupplierName);
        Assert.StrictEqual(expectedUseCase, result.UseCase);
        Assert.StrictEqual(expectedSignatory, result.Signatory);
        Assert.StrictEqual(expectedInteractions, result.Interactions);
    }
   
}