using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GpConnect.NationalDataSharingPortal.Api.Service.Interface;

public interface IUserService
{
    Task<IEnumerable<User>> GetUsers();
}