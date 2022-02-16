using gpconnect_user_portal.Helpers;
using gpconnect_user_portal.Helpers.Constants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace gpconnect_user_portal.DTO.Response.Application
{
    public class SiteDefinition
    {
        public string SiteOdsCode { get; set; }
        public string SitePartyKey { get; set; }
        public string SiteAsid { get; set; }
        public Guid SiteUniqueIdentifier { get; set; }
        public int SiteDefinitionId { get; set; }
        public int SiteDefinitionStatusId { get; set; }
        public bool CanUpdateOrSubmit { get; set; }
        public string SiteInteractions { get; set; }

        public bool NoOdsIssued => string.IsNullOrEmpty(SiteOdsCode);

        [IgnoreDataMember]
        public bool HasHtmlView => SiteInteractions.Contains(SearchConstants.HtmlQueryFilterInteraction);

        [IgnoreDataMember]
        public bool HasStructured => SiteInteractions.Contains(SearchConstants.StructuredQueryFilterInteraction);

        [IgnoreDataMember]
        public bool HasAppointment => SiteInteractions.Contains(SearchConstants.AppointmentQueryFilterInteraction);

        [JsonProperty(DisplayConstants.RECORDACCESSHTMLVIEW)]
        public string HasHtmlViewAsText => HasHtmlView.BooleanToYesNo();
        [JsonProperty(DisplayConstants.RECORDACCESSSTRUCTURED)]
        public string HasStructuredAsText => HasStructured.BooleanToYesNo();
        [JsonProperty(DisplayConstants.APPOINTMENT)]
        public string HasAppointmentAsText => HasAppointment.BooleanToYesNo();

        public List<SiteAttribute> SiteAttributes { get; set; }
    }
}
