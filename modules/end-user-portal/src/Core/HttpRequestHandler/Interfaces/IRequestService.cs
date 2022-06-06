namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpRequestHandler.Interfaces;

public interface IRequestService
{
  Task<TResult> ExecuteApiGetAsync<TResult>(string url) where TResult : class;
  Task<TResult> ExecuteApiGetAsync<TRequest, TResult>(TRequest t, string url) where TRequest : class where TResult : class;
  Task<TResult> ExecuteApiPostAsync<TRequest, TResult>(TRequest t, string url) where TRequest : class where TResult : class;
}
