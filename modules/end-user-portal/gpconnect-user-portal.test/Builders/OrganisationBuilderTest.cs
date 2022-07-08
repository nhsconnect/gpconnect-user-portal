using System.Collections.Generic;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Builders;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Builders.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Request;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Response;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Test.Builders
{
    public class OrganisationBuilderTest
    {
        private OrganisationBuilder _sut;

        public OrganisationBuilderTest()
        {
            _sut = new OrganisationBuilder();
        }

        [Fact]
        public void Build_GivenOrganisationResult_ReturnsExpectedObject()
        {
            const string expectedOdsCode = "Code";
            const string expectedName = "Name";
            const string expectedAD1 = "AddressLine1";
            const string expectedAD2 = "AddressLine2";
            const string expectedCity = "City";
            const string expectedCounty = "County";
            const string expectedCountry = "Country";
            const string expectedPostCode = "PostCode";

            var organisation = new OrganisationResult
            {
                OdsCode = expectedOdsCode,
                Name = expectedName,
                Address = new OrganisationAddress
                {
                    AddressLines = new List<string> {
                        expectedAD1, expectedAD2
                    },
                    City = expectedCity,
                    County = expectedCounty,
                    Country = expectedCountry,
                    Postcode = expectedPostCode
                }
            };
            var result = _sut.Build(organisation);

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
        public void Build_GivenOrganisationResult_WithOnlyOneAddress_ReturnsNullAddressLine2()
        {
            const string expectedAD1 = "AddressLine1";
            
            var organisation = new OrganisationResult
            {
                Address = new OrganisationAddress
                {
                    AddressLines = new List<string> {
                        expectedAD1
                    },
                }
            };
            var result = _sut.Build(organisation);

            Assert.Equal(expectedAD1, result.AddressLine1);
            Assert.Equal(string.Empty, result.AddressLine2);
        }
    }
}