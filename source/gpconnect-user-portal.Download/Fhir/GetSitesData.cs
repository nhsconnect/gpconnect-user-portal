using gpconnect_user_portal.Services.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Download
{
    public partial class GetSitesData
    {
        private readonly IReferenceService _referenceService;
        private readonly IApplicationService _applicationService;

        public GetSitesData(IReferenceService referenceService, IApplicationService applicationService)
        {
            _referenceService = referenceService;
            _applicationService = applicationService;
        }

        [FunctionName("GetSites")]
        public async Task GetSites([TimerTrigger("0 0 5 * * *", RunOnStartup = true)] TimerInfo myTimer, ILogger log)
        {
            var siteDefinitions = await _referenceService.GetSiteDefinitions();
            await _applicationService.AddSiteDefinitionsFromFeed(siteDefinitions);
        }
    }
}
