using gpconnect_user_portal.Helpers.Constants;
using System;
using System.Collections.Generic;

namespace gpconnect_user_portal.DTO.Request
{
    public class SiteDefinition
    {
        public Guid SiteUniqueIdentifier { get; set; }
        public string SiteOdsCode { get; set; }
        public string SupplierOdsCode { get; set; }
        public string SiteAsid { get; set; }
        public string SitePartyKey { get; set; }
        public string SiteInteractions { get; set; }   
        public List<SiteAttribute> SiteAttribute { get; set; }
        public bool IsHtmlEnabled => SiteInteractions != null && SiteInteractions.Contains(SearchConstants.HtmlQueryFilterInteraction);
        public bool IsStructuredEnabled => SiteInteractions != null && SiteInteractions.Contains(SearchConstants.StructuredQueryFilterInteraction);
        public bool IsAppointmentEnabled => SiteInteractions != null && SiteInteractions.Contains(SearchConstants.AppointmentQueryFilterInteraction);
        public bool IsSendDocumentEnabled => SiteInteractions != null && SiteInteractions.Contains(SearchConstants.SendDocumentQueryFilterInteraction);
    }
}
