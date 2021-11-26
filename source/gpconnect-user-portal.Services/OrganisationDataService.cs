using gpconnect_user_portal.DAL.Interfaces;
using gpconnect_user_portal.DTO.Response;
using gpconnect_user_portal.Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Services
{
    public class OrganisationDataService : IOrganisationDataService
    {
        private readonly IFhirRequestExecution _fhirRequestExecution;

        public OrganisationDataService(IFhirRequestExecution fhirRequestExecution)
        {
            _fhirRequestExecution = fhirRequestExecution;
        }

        public async Task<CCG> GetCCGs()
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            var requestUri = new Uri("https://directory.spineservices.nhs.uk/ORD/2-0-0/organisations?Roles=RO98&Limit=1000&Status=Active");
            var response = await _fhirRequestExecution.ExecuteFhirQuery<CCG>(requestUri, token);
            return response;
        }
    }
}
