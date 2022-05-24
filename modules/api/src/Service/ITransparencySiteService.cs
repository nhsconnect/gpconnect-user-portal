using System.Collections.Generic;
using gpconnect_user_portal.api.dto;
using gpconnect_user_portal.api.dto.request;

namespace gpconnect_user_portal.api.service
{
    public interface ITransparencySiteService
    {
        IEnumerable<TransparencySite> GetMatchingSites(TransparencySiteRequest request);
    }
}