using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers;

public static class UriExtensions
{
    public static string AddQueryParamsToObject<TRequest>(string url, TRequest searchRequest, JsonSerializerSettings options) where TRequest : class
    {
        var jsonRequest = JsonConvert.SerializeObject(searchRequest, options);
        var queryParams = JsonConvert.DeserializeObject<Dictionary<string, string?>>(jsonRequest);

        if (queryParams != null)
        {
            url = QueryHelpers.AddQueryString(url, queryParams);
        }
        return url;
    }
}
