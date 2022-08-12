using GpConnect.NationalDataSharingPortal.EndUserPortal.Builders;
using Xunit;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Test.Builders
{
    public class SignatoryBuilderTest
    {
        private SignatoryBuilder _sut;

        public SignatoryBuilderTest()
        {
            _sut = new SignatoryBuilder();
        }

        [Fact] 
        public void Build_GivenValidStrings_ReturnsExpectedObject()
        {
            var expectedName = "TestName";
            var expectedEmail = "TestEmail";
            var expectedPosition = "TestPosition";

            var signatory = _sut.Build(expectedName, expectedEmail, expectedPosition);

            Assert.Equal(expectedName, signatory.Name);
            Assert.Equal(expectedEmail, signatory.Email);
            Assert.Equal(expectedPosition, signatory.Position);
        }
    }
}
