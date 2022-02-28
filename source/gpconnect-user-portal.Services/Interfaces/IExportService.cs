using gpconnect_user_portal.DTO.Request;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Services.Interfaces
{
    public interface IExportService
    {
        Task<DataTable> GetSitesForExport(SearchRequest searchRequest = null);
        MemoryStream CreateReport(DataTable result, string reportName = "");
        FileStreamResult GetFileStream(MemoryStream memoryStream, string fileName = null);
    }
}
