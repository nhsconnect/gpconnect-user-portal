using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Enumerations;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;

public interface ISiteService
{
  Task<SearchResult> SearchSitesAsync(string query, SearchMode mode, int offset, int resultsPerPage);
  Task<SearchResultEntry> SearchSiteAsync(string id);
}
