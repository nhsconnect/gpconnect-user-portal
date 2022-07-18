using GpConnect.NationalDataSharingPortal.EndUserPortal.Builders;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Response;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Test.Builders
{
    public class OrganisationBuilderTest
    {
        private OrganisationBuilder _sut;
        private readonly Mock<IOrganisationLookupService> _mockOrganisationLookupService;

        public OrganisationBuilderTest()
        {
            _mockOrganisationLookupService = new Mock<IOrganisationLookupService>();
            _sut = new OrganisationBuilder(_mockOrganisationLookupService.Object);
        }

        [Fact]
        public async Task Build_GivenOrganisationResult_ReturnsExpectedObject()
        {
            const string expectedOdsCode = "Code";
            const string expectedName = "Name";
            const string expectedAD1 = "AddressLine1";
            const string expectedAD2 = "AddressLine2";
            const string expectedCity = "City";
            const string expectedCounty = "County";
            const string expectedCountry = "Country";
            const string expectedPostCode = "PostCode";

            var organisationResult = new OrganisationResult()
            {
                Address = new OrganisationAddress() {
                    AddressLines = new List<string>() { expectedAD1, expectedAD2 },
                    City = expectedCity,
                    Country = expectedCountry,
                    County = expectedCounty,
                    Postcode = expectedPostCode },
                Name = expectedName,
                OdsCode = expectedOdsCode
            };

            _mockOrganisationLookupService.Setup(x => x.GetOrganisationAsync(It.IsAny<string>())).Returns(Task.FromResult(organisationResult));

            var result = await _sut.Build(expectedOdsCode);

            Assert.Equal(expectedOdsCode, result.OdsCode);
            Assert.Equal(expectedName, result.Name);
            Assert.Equal(expectedAD1, result.AddressLine1);
            Assert.Equal(expectedAD2, result.AddressLine2);
            Assert.Equal(expectedCity, result.Town);
            Assert.Equal(expectedCounty, result.County);
            Assert.Equal(expectedCountry, result.Country);
            Assert.Equal(expectedPostCode, result.PostCode);
        }

        [Fact]
        public async Task Build_GivenOrganisationResult_WithOnlyOneAddress_ReturnsNullAddressLine2()
        {
            const string expectedAD1 = "AddressLine1";

            var organisationResult = new OrganisationResult()
            {
                Address = new OrganisationAddress()
                {
                    AddressLines = new List<string>() { expectedAD1 }
                }
            };
            _mockOrganisationLookupService.Setup(x => x.GetOrganisationAsync(It.IsAny<string>())).Returns(Task.FromResult(organisationResult));

            var result = await _sut.Build("OdsCode");

            Assert.Equal(expectedAD1, result.AddressLine1);
            Assert.Equal(string.Empty, result.AddressLine2);
        }
    }
}
