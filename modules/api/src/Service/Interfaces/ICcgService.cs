using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GpConnect.NationalDataSharingPortal.Api.Service.Interface;

public interface ICcgService
{
    Task<IEnumerable<Ccg>> GetCcgs();
}