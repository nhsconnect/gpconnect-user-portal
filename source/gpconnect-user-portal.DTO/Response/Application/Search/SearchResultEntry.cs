using gpconnect_user_portal.Helpers.Constants;
using gpconnect_user_portal.Resources;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace gpconnect_user_portal.DTO.Response.Application.Search
{
    public class SearchResultEntry
    {
        [JsonIgnore]
        public int SiteDefinitionId { get; set; }

        [Display(Name = "OdsCode", ResourceType = typeof(DataFieldNameResources))]
        public string SiteODSCode { get; set; }

        [JsonIgnore]
        public Guid SiteUniqueIdentifier { get; set; }

        [JsonIgnore]
        public int SiteDefinitionStatusId { get; set; }

        [JsonIgnore]
        public string SiteInteractions { get; set; }

        public string SiteName { get; set; }
        public string SelectedCCGOdsCode { get; set; }
        public string SelectedCCGName { get; set; }
        [JsonIgnore] 
        public bool IsAppointmentEnabled { get; set; }
        [JsonIgnore]
        public bool IsHtmlEnabled { get; set; }
        [JsonIgnore]
        public bool IsStructuredEnabled { get; set; }
        [JsonIgnore]
        public bool IsSendDocumentEnabled { get; set; }
        [JsonIgnore]
        public string SitePostcode { get; set; }
        [JsonIgnore]
        public string OdsCode { get; set; }
        [JsonIgnore]
        public string SelectedSupplier { get; set; }
        public string UseCaseDescription { get; set; }

        [Display(Name = DisplayConstants.HASHTMLVIEW)]
        public bool HasHtmlView => SiteInteractions != null && SiteInteractions.Contains(SearchConstants.HtmlQueryFilterInteraction);

        [Display(Name = DisplayConstants.HASSTRUCTURED)] 
        public bool HasStructured => SiteInteractions != null && SiteInteractions.Contains(SearchConstants.StructuredQueryFilterInteraction);

        [Display(Name = DisplayConstants.HASAPPOINTMENT)]
        public bool HasAppointment => SiteInteractions != null && SiteInteractions.Contains(SearchConstants.AppointmentQueryFilterInteraction);

        [Display(Name = DisplayConstants.HASSENDDOCUMENT)]
        public bool HasSendDocument => SiteInteractions != null && SiteInteractions.Contains(SearchConstants.SendDocumentQueryFilterInteraction);
    }
}
