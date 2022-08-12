using GpConnect.NationalDataSharingPortal.EndUserPortal.Builders;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Builders.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Request;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Test.Builders;

public class AgreementInformationBuilderTest
{
    private readonly Mock<IOrganisationBuilder> _mockOrganisationBuilder;
    private readonly Mock<ISupplierBuilder> _mockSupplierBuilder;
    private readonly Mock<IInteractionsBuilder> _mockInteractionsBuilder;
    private readonly Mock<ISignatoryBuilder> _mockSignatoryBuilder;

    private readonly AgreementInformationBuilder _sut;

    public AgreementInformationBuilderTest()
    {
        _mockOrganisationBuilder = new Mock<IOrganisationBuilder>();
        _mockSupplierBuilder = new Mock<ISupplierBuilder>();
        _mockInteractionsBuilder = new Mock<IInteractionsBuilder>();
        _mockSignatoryBuilder = new Mock<ISignatoryBuilder>();

        _sut = new AgreementInformationBuilder(_mockOrganisationBuilder.Object, _mockSupplierBuilder.Object, _mockInteractionsBuilder.Object, _mockSignatoryBuilder.Object);
    }

    [Fact]
    public void Build_CallsOrganisationBuilder_WithExpectedParameters()
    {
        var organisationOdsCode = "A20047";

        _sut.Build(organisationOdsCode, null, null, null, null, null, null);

        _mockOrganisationBuilder.Verify(mob => mob.Build(organisationOdsCode), Times.Once);
    }

    [Fact]
    public void Build_CallsSignatoryBuilder_WithExpectedParameters()
    {
        var expectedName = "Name";
        var expectedEmail = "Email";
        var expectedPosition = "Position";

        _sut.Build(null, "1", null, null, expectedName, expectedEmail, expectedPosition);

        _mockSignatoryBuilder.Verify(msb => msb.Build(expectedName, expectedEmail, expectedPosition), Times.Once);
    }

    [Fact]
    public void Build_CallsInteractionsBuilder_WithExpectedParameters()
    {
        var expectedInteractions = new List<EndUserPortal.Helpers.Constants.GpConnectInteractions>();

        _sut.Build(null, "1", null, expectedInteractions, null, null, null);

        _mockInteractionsBuilder.Verify(mib => mib.Build(expectedInteractions), Times.Once);
    }

    [Fact]
    public void Build_OrganisationBuilderThrows_Throws()
    {
        _mockOrganisationBuilder.Setup(mob => mob.Build(It.IsAny<string>())).ThrowsAsync(new Exception("Boom!!!"));

        Assert.ThrowsAsync<Exception>(() => _sut.Build(null, null, null, null, null, null, null));
    }

    [Fact]
    public async Task Build_SignatoryBuilderThrows_Throws()
    {
        _mockSignatoryBuilder.Setup(msb => msb.Build(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception("Boom!!!"));

        await Assert.ThrowsAsync<Exception>(() => _sut.Build(null, "1", null, null, null, null, null));
    }


    [Fact]
    public async Task Build_InteractionsBuilderThrows_Throws()
    {
        _mockInteractionsBuilder.Setup(mib => mib.Build(It.IsAny<List<EndUserPortal.Helpers.Constants.GpConnectInteractions>>())).Throws(new Exception("Boom!!!"));

        await Assert.ThrowsAsync<Exception>(() => _sut.Build(null, "1", null, null, null, null, null));
    }

    [Fact]
    public async void Build_GivenBuildersReturn_ReturnsExpectedObject()
    {
        var expectedInteractionsResponse = new GpConnectInteractions();
        var expectedSupplier = new SupplierInformation() { Name = "" };
        var expectedUseCase = "UseCase";
        var expectedOrganisation = new OrganisationInformation();
        var expectedSignatory = new SignatoryDetails();

        _mockInteractionsBuilder.Setup(mib => mib.Build(It.IsAny<List<EndUserPortal.Helpers.Constants.GpConnectInteractions>>())).Returns(expectedInteractionsResponse);
        _mockOrganisationBuilder.Setup(mob => mob.Build(It.IsAny<string>())).Returns(Task.FromResult(expectedOrganisation));
        _mockSignatoryBuilder.Setup(mib => mib.Build(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(expectedSignatory);
        _mockSupplierBuilder.Setup(mib => mib.Build(It.IsAny<int>())).Returns(Task.FromResult(expectedSupplier));

        var result = await _sut.Build(null, "1", expectedUseCase, null, null, null, null);

        Assert.StrictEqual(expectedOrganisation, result.Organisation);
        Assert.StrictEqual(expectedSupplier, result.SoftwareSupplier);
        Assert.StrictEqual(expectedUseCase, result.UseCase);
        Assert.StrictEqual(expectedSignatory, result.Signatory);
        Assert.StrictEqual(expectedInteractionsResponse, result.Interactions);
    }   
}
