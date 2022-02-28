using gpconnect_user_portal.Helpers.Constants;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace gpconnect_user_portal.DTO.Response.Application.Search
{
    public class SearchResultEntry
    {
        public int SiteDefinitionId { get; set; }

        [Display(Name = DisplayConstants.SITEODSCODE)]
        public string SiteODSCode { get; set; }

        [JsonIgnore]
        public Guid SiteUniqueIdentifier { get; set; }

        [JsonIgnore]
        public int SiteDefinitionStatusId { get; set; }

        [JsonIgnore]
        public string SiteInteractions { get; set; }

        [JsonIgnore]
        public string SiteAttributesArray { get; set; }

        public string[] SiteAttributes => SiteAttributesArray.Split(",");

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
