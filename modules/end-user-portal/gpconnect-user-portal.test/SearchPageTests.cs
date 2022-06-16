using GpConnect.NationalDataSharingPortal.EndUserPortal.Core;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using Microsoft.Extensions.Options;
using Moq;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Test
{
    public class SearchPageTests
  {
    private readonly Mock<IOptions<ApplicationParameters>> _mockOptions;

    public SearchPageTests()
    {
      _mockOptions = new Mock<IOptions<ApplicationParameters>>();
    }    
  }
}
