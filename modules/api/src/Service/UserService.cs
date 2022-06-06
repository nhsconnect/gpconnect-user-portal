using GpConnect.NationalDataSharingPortal.Api.Dal.Interfaces;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using GpConnect.NationalDataSharingPortal.Api.Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GpConnect.NationalDataSharingPortal.Api.Service;

public class UserService : IUserService
{
    private readonly IDataService _dataService;

    public UserService(IDataService dataService)
    {
        _dataService = dataService;
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        var query = "application.get_users";
        var result = await _dataService.ExecuteQuery<User>(query);
        return result;
    }
}