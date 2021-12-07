using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IO;

namespace gpconnect_user_portal.Services.Interfaces
{
    public interface IReportingService
    {
        MemoryStream CreateReport(DataTable result, string reportName = "");
        FileStreamResult GetFileStream(MemoryStream memoryStream, string fileName = null);
    }
}
