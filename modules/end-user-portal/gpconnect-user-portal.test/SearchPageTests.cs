using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using Microsoft.Extensions.Options;
using Moq;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Test
{
    public class SearchPageTests
  {
    private readonly Mock<IOptions<Core.ApplicationParameters>> _mockOptions;
    private readonly Mock<ISiteService> _mockSiteService;

    public SearchPageTests()
    {
      _mockOptions = new Mock<IOptions<Core.ApplicationParameters>>();
      _mockSiteService = new Mock<ISiteService>();
    }    
  }
}
