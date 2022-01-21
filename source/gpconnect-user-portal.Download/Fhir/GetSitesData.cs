using gpconnect_user_portal.Services.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Download
{
    public partial class GetSitesData
    {
        private readonly IReferenceService _referenceService;

        public GetSitesData(IReferenceService referenceService)
        {
            _referenceService = referenceService;
        }

        [FunctionName("GetSites")]
        public async Task GetSites([TimerTrigger("0 0 5 * * *", RunOnStartup = true)] TimerInfo myTimer, ILogger log)
        {
            var sites = await _referenceService.GetSites();
        }
    }
}
