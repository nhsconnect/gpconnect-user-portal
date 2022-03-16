using Dapper.FluentMap.Mapping;
using gpconnect_user_portal.DTO.Response.Configuration;

namespace gpconnect_user_portal.DAL.Mapping
{
    public class ReferenceConfigurationMap : EntityMap<Reference>
    {
        public ReferenceConfigurationMap()
        {
            Map(p => p.HostName).ToColumn("host_name");
        }
    }
}
