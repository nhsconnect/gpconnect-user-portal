using gpconnect_user_portal.Helpers;
using gpconnect_user_portal.Helpers.Constants;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace gpconnect_user_portal.DTO.Response
{
    public class SearchResultEntry
    {
        [Display(Name = DisplayConstants.SITENAME)]
        [JsonProperty(DisplayConstants.SITENAME)]
        public string SiteName { get; set; }
        [Display(Name = DisplayConstants.SITEODSCODE)]
        [JsonProperty(DisplayConstants.SITEODSCODE)]
        public string SiteODSCode { get; set; }
        [Display(Name = DisplayConstants.CCGICBNAME)]
        [JsonProperty(DisplayConstants.CCGICBNAME)]
        public string CCGName { get; set; }
        [Display(Name = DisplayConstants.CCGICBODSCODE)]
        [JsonProperty(DisplayConstants.CCGICBODSCODE)]
        public string CCGODSCode { get; set; }
        [IgnoreDataMember]
        public string Interactions { get; set; }
        [Display(Name = DisplayConstants.RECORDACCESSHTMLVIEW)]
        [IgnoreDataMember]
        public bool HasHtmlView => Interactions.Contains("gpc.getcarerecord");

        [JsonProperty(DisplayConstants.RECORDACCESSHTMLVIEW)]
        public string HasHtmlViewAsText => HasHtmlView.BooleanToYesNo();
        [JsonProperty(DisplayConstants.RECORDACCESSSTRUCTURED)]
        public string HasStructuredAsText => HasStructured.BooleanToYesNo();
        [JsonProperty(DisplayConstants.APPOINTMENT)]
        public string HasAppointmentAsText => HasAppointment.BooleanToYesNo();

        [Display(Name = DisplayConstants.RECORDACCESSSTRUCTURED)]
        [IgnoreDataMember]
        public bool HasStructured => Interactions.Contains("structured:fhir:rest:read:metadata-1");
        [Display(Name = DisplayConstants.APPOINTMENT)]
        [IgnoreDataMember]
        public bool HasAppointment => Interactions.Contains("appointments-1");
        [Display(Name = DisplayConstants.USECASE)]
        [JsonProperty(DisplayConstants.USECASE)]
        public string UseCase { get; set; }
        [IgnoreDataMember]
        public bool DisplayUseCase => !string.IsNullOrEmpty(UseCase);        
    }
}
