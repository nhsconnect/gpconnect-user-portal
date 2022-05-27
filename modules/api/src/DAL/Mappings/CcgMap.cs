using Dapper.FluentMap.Mapping;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;

namespace GpConnect.NationalDataSharingPortal.Api.Dal.Mapping
{
    public class CcgMap : EntityMap<Ccg>
    {
        public CcgMap()
        {
            Map(p => p.CcgId).ToColumn("lookup_id");
            Map(p => p.CcgLinkedId).ToColumn("linked_lookup_id");
            Map(p => p.CcgOdsCode).ToColumn("lookup_value");
            Map(p => p.CcgName).ToColumn("linked_lookup_value");
        }
    }
}
