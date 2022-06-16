using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;

public interface ISiteService
{
  Task<List<SearchResultEntry>> SearchSitesAsync(SearchRequest searchRequest);
  Task<SearchResultEntry> SearchSiteAsync(string id);
}
