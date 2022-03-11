using Dapper.FluentMap.Mapping;
using gpconnect_user_portal.DTO.Response.Configuration;

namespace gpconnect_user_portal.DAL.Mapping
{
    public class LoggingConfigurationMap : EntityMap<Logging>
    {
        public LoggingConfigurationMap()
        {
            Map(p => p.Token).ToColumn("token");
            Map(p => p.Channel).ToColumn("channel");
            Map(p => p.LogsIndex).ToColumn("logs_index");
            Map(p => p.WebIndex).ToColumn("web_index");
            Map(p => p.ErrorIndex).ToColumn("error_index");
            Map(p => p.ServerUrl).ToColumn("server_url");
            Map(p => p.Source).ToColumn("source");
            Map(p => p.SourceType).ToColumn("source_type");
            Map(p => p.UseProxy).ToColumn("use_proxy");
            Map(p => p.ProxyUrl).ToColumn("proxy_url");
            Map(p => p.ProxyUser).ToColumn("proxy_user");
            Map(p => p.ProxyPassword).ToColumn("proxy_password");
        }
    }
}
