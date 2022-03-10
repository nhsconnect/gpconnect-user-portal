using System;

namespace gpconnect_user_portal.DTO.Response.Configuration
{
    public class Logging
    {
        public string ServerUrl { get; set; }
        public string Token { get; set; }
        public Guid Channel { get; set; }
        public string Index { get; set; }
        public string Source { get; set; }
        public string SourceType { get; set; }
        public bool UseProxy { get; set; }
        public string ProxyUrl { get; set; }
        public string ProxyUser { get; set; }
        public string ProxyPassword { get; set; }
    }
}
