using Dapper;
using gpconnect_user_portal.DAL.Interfaces;
using gpconnect_user_portal.DTO.Request.Logging;
using gpconnect_user_portal.Services.Interfaces;
using System;

namespace gpconnect_user_portal.Services
{
    public class LogService : ILogService
    {
        private readonly IDataService _dataService;

        public LogService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public void AddWebRequestLog(WebRequest webRequest)
        {
            var query = "";
            var parameters = new DynamicParameters();

            parameters.Add("_url", webRequest.Url);
            parameters.Add("_referrer_url", webRequest.ReferrerUrl);
            parameters.Add("_description", webRequest.Description);
            parameters.Add("_ip", webRequest.Ip);
            parameters.Add("_created_date", DateTime.UtcNow);
            parameters.Add("_server", webRequest.Server);
            parameters.Add("_response_code", webRequest.ResponseCode);
            parameters.Add("_session_id", webRequest.SessionId);
            parameters.Add("_user_agent", webRequest.UserAgent);

            _dataService.ExecuteQuery(query, parameters);
        }
    }
}
