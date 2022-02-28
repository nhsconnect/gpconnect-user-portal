using gpconnect_user_portal.Helpers.Constants;
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
        public DateTime SubmittedDate { get; set; }
        public string SiteDefinitionStatusName { get; set; }

        public bool NoOdsIssued => string.IsNullOrEmpty(SiteOdsCode);

        [IgnoreDataMember]
        public bool HasHtmlView => SiteInteractions.Contains(SearchConstants.HtmlQueryFilterInteraction);

        [IgnoreDataMember]
        public bool HasStructured => SiteInteractions.Contains(SearchConstants.StructuredQueryFilterInteraction);

        [IgnoreDataMember]
        public bool HasAppointment => SiteInteractions.Contains(SearchConstants.AppointmentQueryFilterInteraction);

        public List<SiteAttribute> SiteAttributes { get; set; }
    }
}
