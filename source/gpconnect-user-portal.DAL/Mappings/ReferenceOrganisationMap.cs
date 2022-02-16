using Dapper.FluentMap.Mapping;
using gpconnect_user_portal.DTO.Response.Reference;
using System;

namespace gpconnect_user_portal.DAL.Mapping
{
    public class ReferenceOrganisationMap : EntityMap<Organisation>
    {
        public ReferenceOrganisationMap()
        {
            Map(p => p.OrganisationId).ToColumn("organisation_id");
            Map(p => p.AddedDate).ToColumn("added_date");
            Map(p => p.LastChangeDate).ToColumn("last_change_date");
            Map(p => p.LastSyncDate).ToColumn("last_sync_date");
            Map(p => p.OdsCode).ToColumn("ods_code");
            Map(p => p.OrgRecordClass).ToColumn("org_record_class");
            Map(p => p.Status).ToColumn("org_status");
            Map(p => p.OrgLink).ToColumn("organisation_link");
            Map(p => p.Name).ToColumn("organisation_name");
            Map(p => p.Postcode).ToColumn("postcode");
            Map(p => p.PrimaryRoleDescription).ToColumn("primary_role_description");
            Map(p => p.PrimaryRoleId).ToColumn("primary_role_id");
        }
    }
}
