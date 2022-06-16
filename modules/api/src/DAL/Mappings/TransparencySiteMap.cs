using Dapper.FluentMap.Mapping;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;

namespace GpConnect.NationalDataSharingPortal.Api.Dal.Mapping
{
    public class TransparencySiteMap : EntityMap<TransparencySite>
    {
        public TransparencySiteMap()
        {
            Map(p => p.Id).ToColumn("site_unique_identifier");
            Map(p => p.Name).ToColumn("site_name");
            Map(p => p.OdsCode).ToColumn("site_ods_code");
            Map(p => p.Line1).ToColumn("site_address_line_1");
            Map(p => p.Line2).ToColumn("site_address_line_2");
            Map(p => p.Town).ToColumn("site_address_town");
            Map(p => p.County).ToColumn("site_address_county");
            Map(p => p.Country).ToColumn("site_address_country");
            Map(p => p.Postcode).ToColumn("site_postcode");
            Map(p => p.UseCase).ToColumn("use_case_description");
            Map(p => p.CcgIcbName).ToColumn("selected_ccg_name");
            Map(p => p.CcgIcbOdsCode).ToColumn("selected_ccg_ods_code");
            Map(p => p.AppointmentManagementEnabled).ToColumn("is_appointment_enabled");
            Map(p => p.AccessRecordHTMLEnabled).ToColumn("is_html_enabled");
            Map(p => p.StructuredRecordEnabled).ToColumn("is_structured_enabled");
            Map(p => p.SendDocumentEnabled).ToColumn("is_send_document_enabled");
        }
    }
}
