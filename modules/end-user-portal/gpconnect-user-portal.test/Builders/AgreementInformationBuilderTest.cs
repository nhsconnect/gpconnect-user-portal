using GpConnect.NationalDataSharingPortal.EndUserPortal.Builders;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Builders.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
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
    private readonly Mock<IInteractionsBuilder> _mockInteractionsBuilder;
    private readonly Mock<ISignatoryBuilder> _mockSignatoryBuilder;
    private readonly Mock<IOrganisationLookupService> _mockOrganisationLookupService;

    private readonly AgreementInformationBuilder _sut;

    public AgreementInformationBuilderTest()
    {
        _mockOrganisationBuilder = new Mock<IOrganisationBuilder>();
        _mockInteractionsBuilder = new Mock<IInteractionsBuilder>();
        _mockSignatoryBuilder = new Mock<ISignatoryBuilder>();
        _mockOrganisationLookupService = new Mock<IOrganisationLookupService>();

        _sut = new AgreementInformationBuilder(_mockOrganisationBuilder.Object, _mockInteractionsBuilder.Object, _mockSignatoryBuilder.Object, _mockOrganisationLookupService.Object);
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

        _sut.Build(null, null, null, null, expectedName, expectedEmail, expectedPosition);

        _mockSignatoryBuilder.Verify(msb => msb.Build(expectedName, expectedEmail, expectedPosition), Times.Once);
    }

    [Fact]
    public void Build_CallsInteractionsBuilder_WithExpectedParameters()
    {
        var expectedInteractions = new List<int>();

        _sut.Build(null, null, null, expectedInteractions, null, null, null);

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

        await Assert.ThrowsAsync<Exception>(() => _sut.Build(null, null, null, null, null, null, null));
    }


    [Fact]
    public async Task Build_InteractionsBuilderThrows_Throws()
    {
        _mockInteractionsBuilder.Setup(mib => mib.Build(It.IsAny<List<int>>())).Throws(new Exception("Boom!!!"));

        await Assert.ThrowsAsync<Exception>(() => _sut.Build(null, null, null, null, null, null, null));
    }

    [Fact]
    public async void Build_GivenBuildersReturn_ReturnsExpectedObject()
    {
        var expectedInteractionsResponse = new Models.Request.GpConnectInteractions();
        var expectedSupplierName = "Supplier";
        var expectedUseCase = "UseCase";
        var expectedOrganisation = new OrganisationInformation();
        var expectedSignatory = new SignatoryDetails();

        _mockInteractionsBuilder.Setup(mib => mib.Build(It.IsAny<List<int>>())).Returns(expectedInteractionsResponse);
        _mockOrganisationBuilder.Setup(mob => mob.Build(It.IsAny<string>())).Returns(Task.FromResult(expectedOrganisation));
        _mockSignatoryBuilder.Setup(mib => mib.Build(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(expectedSignatory);


        var result = await _sut.Build(null, expectedSupplierName, expectedUseCase, null, null, null, null);

        Assert.StrictEqual(expectedOrganisation, result.Organisation);
        Assert.StrictEqual(expectedSupplierName, result.SoftwareSupplierName);
        Assert.StrictEqual(expectedUseCase, result.UseCase);
        Assert.StrictEqual(expectedSignatory, result.Signatory);
        Assert.StrictEqual(expectedInteractionsResponse, result.Interactions);
    }

}
