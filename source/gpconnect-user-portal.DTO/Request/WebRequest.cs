using Microsoft.AspNetCore.Http;
using System.Net;

namespace gpconnect_user_portal.DTO.Request.Logging
{
    public class WebRequest
    {
        public string Url { get; set; }
        public string ReferrerUrl { get; set; }
        public string Method { get; set; }
        public string Ip { get; set; }
        public HostString? Host { get; set; }
        public int? StatusCode { get; set; }
        public string SessionId { get; set; }
        public string UserAgent { get; set; }
    }
}
