using System.Collections.Generic;
using gpconnect_user_portal.api.dto;
using gpconnect_user_portal.api.dto.request;

namespace gpconnect_user_portal.api.service
{
    public class TransparencySiteService: ITransparencySiteService
    {
        public IEnumerable<TransparencySite> GetMatchingSites(TransparencySiteRequest request) => new List<TransparencySite> 
        {
            { 
                new TransparencySite
                {
                    Name = "Site1",
                    OdsCode = "ODS1",
                    UseCase = "Some Use Case"
                }
            },
            {
                new TransparencySite
                {
                    Name = "Site2",
                    OdsCode = "ODS2",
                    UseCase = "Some Other Use Case"
                }
            }
        };
    }
}