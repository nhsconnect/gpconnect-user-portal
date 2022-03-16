using gpconnect_user_portal.DTO.Request;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace gpconnect_user_portal.DTO.Response.Fhir
{
    public class OrganisationsWithInteractions
    {
        [JsonProperty("entry")]
        public List<Entry> Entry { get; set; }

        public List<SiteDefinition> SiteDefinitions => GenerateSiteDefinitions();

        private List<SiteDefinition> GenerateSiteDefinitions()
        {
            var siteDefinitions = new List<SiteDefinition>();

            foreach (var siteDefinition in Entry)
            {
                siteDefinitions.Add(new SiteDefinition
                {
                    SiteOdsCode = siteDefinition?.Resource?.Owner?.Identifier?.Value,
                    SiteAsid = siteDefinition?.Resource?.Identifier?.FirstOrDefault(x => x.System.Contains("nhsSpineASID"))?.Value,
                    SitePartyKey = siteDefinition?.Resource?.Identifier?.FirstOrDefault(x => x.System.Contains("nhsMhsPartyKey"))?.Value,
                    SupplierOdsCode = siteDefinition?.Resource?.Extension?.FirstOrDefault(x => x.Url.Contains("ManufacturingOrganisation"))?.ValueReference?.Identifier?.Value,
                    SiteInteractions = GetInteractions(siteDefinition?.Resource?.Extension),
                    SiteUniqueIdentifier = System.Guid.NewGuid()
                });
            }
            return siteDefinitions;
        }

        private string GetInteractions(List<Extension> extension)
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
