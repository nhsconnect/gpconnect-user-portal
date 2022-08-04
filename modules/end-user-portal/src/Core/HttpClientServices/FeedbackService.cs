using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Request;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices;

public class FeedbackService : IFeedbackService
{
    private readonly HttpClient _client;

    public FeedbackService(HttpClient client, IOptions<FeedbackServiceConfig> options)
    {
        _client = client;
        _client.BaseAddress = new UriBuilder(options.Value.BaseUrl).Uri;
    }

    public async Task SubmitFeedbackAsync(string overallRating, string improveService)
    {
        var content = new StringContent
        (
            JsonConvert.SerializeObject(new FeedbackInformation { OverallRating = overallRating, ImproveService = improveService })
        );

        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var response = await _client.PostAsync("/feedback", content);

        response.EnsureSuccessStatusCode();
    }

    public class FeedbackServiceConfig
    {
        public string BaseUrl { get; set; } = "";
    }
}
