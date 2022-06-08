namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;

public interface ISiteService
{
  Task<TResult> SearchSitesAsync<TRequest, TResult>(TRequest t) where TRequest : class where TResult : class;  
}
