using Dapper.FluentMap.Mapping;
using gpconnect_user_portal.DTO.Response.Application.Search;

namespace gpconnect_user_portal.DAL.Mappings
{
    public class SearchResultEntryMap : EntityMap<SearchResultEntry>
    {
        public SearchResultEntryMap()
        {
            Map(p => p.SiteDefinitionId).ToColumn("site_definition_id");
            Map(p => p.SiteODSCode).ToColumn("site_ods_code");
            Map(p => p.SiteUniqueIdentifier).ToColumn("site_unique_identifier");
            Map(p => p.SiteDefinitionStatusId).ToColumn("site_definition_status_id");
            Map(p => p.SiteInteractions).ToColumn("site_interactions");
            Map(p => p.SiteName).ToColumn("site_name");
            Map(p => p.SelectedCCGOdsCode).ToColumn("selected_ccg_ods_code");
            Map(p => p.SelectedCCGName).ToColumn("selected_ccg_name");
            Map(p => p.IsAppointmentEnabled).ToColumn("is_appointment_enabled");
            Map(p => p.IsHtmlEnabled).ToColumn("is_html_enabled");
            Map(p => p.IsStructuredEnabled).ToColumn("is_structured_enabled");
            Map(p => p.IsSendDocumentEnabled).ToColumn("is_send_document_enabled");
            Map(p => p.SitePostcode).ToColumn("site_postcode");
            Map(p => p.OdsCode).ToColumn("ods_code");
            Map(p => p.SelectedSupplier).ToColumn("selected_supplier");
            Map(p => p.UseCaseDescription).ToColumn("use_case_description");
            Map(p => p.SiteDefinitionStatusName).ToColumn("site_definition_status_name");            
        }
    }
}
