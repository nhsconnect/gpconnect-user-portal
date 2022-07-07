using Dapper.FluentMap.Mapping;
using GpConnect.NationalDataSharingPortal.Api.Dal.Models;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;

namespace GpConnect.NationalDataSharingPortal.Api.Dal.Mapping
{
    public class SiteDefinitionMap : EntityMap<SiteDefinition>
    {
        public SiteDefinitionMap()
        {
            Map(s => s.Id).ToColumn("site_definition_id");
            Map(s => s.OdsCode).ToColumn("site_ods_code");
            Map(s => s.PartyKey).ToColumn("site_party_key");
            Map(s => s.Asid).ToColumn("site_asid");
            Map(s => s.UniqueId).ToColumn("site_unique_identifier");
            Map(s => s.Status).ToColumn("site_definition_status_id");
            Map(s => s.GpConnectInteractions).ToColumn("site_interactions");
            Map(s => s.MasterSiteId).ToColumn("master_site_unique_identifier");
        }
    }
}
