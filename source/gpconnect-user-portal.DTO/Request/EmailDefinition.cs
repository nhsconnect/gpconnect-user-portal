using System;
using System.Data;

namespace gpconnect_user_portal.DTO.Request
{
    public class EmailDefinition
    {
        public Guid SiteUniqueIdentifier { get; set; }
        public DataTable SiteDefinition { get; set; }
        public DataTable SiteAttributes { get; set; }
    }
}
