using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace gpconnect_user_portal.DTO.Response.Fhir
{
    public class OrganisationsWithInteractions
    {
        [JsonProperty("entry")]
        public List<Entry> Entry { get; set; }

        public List<Site> Site => GetListOfSites();

        private List<Site> GetListOfSites()
        {
            var siteList = new List<Site>();

            foreach (var site in Entry)
            {
                siteList.Add(new Site
                {
                    OdsCode = site.Resource.Owner.Identifier.Value,
                    SpineASID = site.Resource.Identifier.FirstOrDefault(x => x.System.Contains("nhsSpineASID")).Value,
                    PartyKey = site.Resource.Identifier.FirstOrDefault(x => x.System.Contains("nhsMhsPartyKey")).Value,
                    SupplierOdsCode = site.Resource.Extension.FirstOrDefault(x => x.Url.Contains("ManufacturingOrganisation")).ValueReference.Identifier.Value,
                    ServiceInteractions = GetServiceInteractionsForSite(site.Resource.Extension)
                });
            }
            return siteList;
        }

        private string GetServiceInteractionsForSite(List<Extension> extension)
        {
            var serviceInteractions = new List<string>();

            foreach (var extensionItem in extension.Where(x => x.ValueReference.Identifier.Value.Contains("urn:nhs:names:services:gpconnect")))
            {
                serviceInteractions.Add(extensionItem.ValueReference.Identifier.Value);
            }
            return string.Join("_", serviceInteractions);
        }
    }
}
