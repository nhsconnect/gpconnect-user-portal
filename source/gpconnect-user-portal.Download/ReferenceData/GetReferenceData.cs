using gpconnect_user_portal.Services.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace gpconnect_user_portal.Download
{
    public partial class GetReferenceData
    {
        private readonly IReferenceService _referenceService;

        public GetReferenceData(IReferenceService referenceService)
        {
            _referenceService = referenceService;
        }
        
        [FunctionName("GetCCGData")]
        public void GetCCGData([TimerTrigger("0 0 4 * * *", RunOnStartup = false)] TimerInfo myTimer, ILogger log)
        {
            _referenceService.GetCCGs();
        }
    }
}
