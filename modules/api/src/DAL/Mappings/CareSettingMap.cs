using Dapper.FluentMap.Mapping;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;

namespace GpConnect.NationalDataSharingPortal.Api.Dal.Mapping
{
    public class CareSettingMap : EntityMap<CareSetting>
    {
        public CareSettingMap()
        {
            Map(p => p.CareSettingId).ToColumn("lookup_id");
            Map(p => p.CareSettingValue).ToColumn("lookup_value");
            Map(p => p.CareSettingName).ToColumn("lookup_type_name");
            Map(p => p.CareSettingDescription).ToColumn("lookup_type_description");
        }
    }
}
