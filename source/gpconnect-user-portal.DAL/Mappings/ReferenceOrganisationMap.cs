using Dapper.FluentMap.Mapping;
using gpconnect_user_portal.DTO.Response.Reference;

namespace gpconnect_user_portal.DAL.Mapping
{
    public class ReferenceOrganisationMap : EntityMap<Organisation>
    {
        public ReferenceOrganisationMap()
        {
            Map(p => p.OdsCode).ToColumn("ods_code");
            Map(p => p.Name).ToColumn("organisation_name");            
        }
    }
}
