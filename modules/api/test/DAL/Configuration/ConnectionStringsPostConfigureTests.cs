using System;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Xunit;
using Moq;

using GpConnect.NationalDataSharingPortal.Api.Dal.Configuration;
using GpConnect.NationalDataSharingPortal.Api.Dal.Authentication.Interface;

namespace GpConnect.NationalDataSharingPortal.Api.Test.Dal.Configuration
{
    public class ConnectionStringsPostConfigurationTests
    {
        private readonly Mock<IAuthTokenGenerator>  _tokenGeneratorMock;


        private readonly IPostConfigureOptions<ConnectionStrings> _sut;
        
        public ConnectionStringsPostConfigurationTests()
        {
            _tokenGeneratorMock = new Mock<IAuthTokenGenerator>(); 
            _sut = new ConnectionStringsPostConfiguration(_tokenGeneratorMock.Object, Mock.Of<ILogger<ConnectionStringsPostConfiguration>>());
        }

        [Fact]
        public void PostConfigure_ConnectionStringDoesNotContainRdsToken_DoesNotCallGenerateToken()
        {
            _sut.PostConfigure(string.Empty, new ConnectionStrings {
                DefaultConnection = "NoToken"
            });

            _tokenGeneratorMock.Verify(_mock => _mock.GenerateAuthToken(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void PostConfigure_ConnectionStringDoesContainRdsToken_CallsGenerateToken_WithExpectedParameters()
        {
            var expectedHost = "ExpectedHost";
            var expectedUser = "ExpectedUser";

            _sut.PostConfigure(string.Empty, new ConnectionStrings {
                DefaultConnection = $"${{rdsToken}};Host={expectedHost};User={expectedUser}"
            });

            _tokenGeneratorMock.Verify(_mock => _mock.GenerateAuthToken(expectedHost, 5432, expectedUser), Times.Once);
        }

        [Fact]
        public void PostConfigure_ConnectionStringDoesContainRdsToken_UpdateConfigWithSubstitutedString()
        {
            _tokenGeneratorMock.Setup(t => t.GenerateAuthToken(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).Returns("TokenMcToken");
            
            var configuration = new ConnectionStrings {
                DefaultConnection = "${rdsToken};Host=expectedHost;User=expectedUser"
            };

            _sut.PostConfigure(string.Empty, configuration);

            Assert.DoesNotContain("{rdsToken}", configuration.DefaultConnection);
            Assert.Contains("TokenMcToken", configuration.DefaultConnection);
        }
    }
}
