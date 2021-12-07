using System;

namespace gpconnect_user_portal.DTO.Response
{
    public class Organisation
    {
        public string Name { get; set; }
        public string OrgId { get; set; }
        public string Status { get; set; }
        public string OrgRecordClass { get; set; }
        public string Postcode { get; set; }
        public DateTime LastChangeDate { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime LastSyncDate { get; set; }
        public string PrimaryRoleId { get; set; }
        public string PrimaryRoleDescription { get; set; }
        public string OrgLink { get; set; }
    }
}
