using gpconnect_user_portal.DTO.Response.Application;

namespace gpconnect_user_portal.Model
{
    public class EndpointChanges
    {
        public EndpointChanges()
        {
            EndpointChange = new List<EndpointChange>();
        }

        public List<EndpointChange> EndpointChange { get; set; }
    }
}