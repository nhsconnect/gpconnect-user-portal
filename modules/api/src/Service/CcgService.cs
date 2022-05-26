using Dapper;
using GpConnect.NationalDataSharingPortal.Api.Dal.Interfaces;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using GpConnect.NationalDataSharingPortal.Api.Service.Interface;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace GpConnect.NationalDataSharingPortal.Api.Service;

public class CcgService : ICcgService
{
    private readonly IDataService _dataService;

    public CcgService(IDataService dataService)
    {
        _dataService = dataService;
    }

    public async Task<IEnumerable<Ccg>> GetCcgs()
    {
        var query = "reference.get_lookup";
        var parameters = new DynamicParameters();
        parameters.Add("_lookup_type_id", (int)Dal.Enumerations.LookupType.CCGICBODSCode, DbType.Int16, ParameterDirection.Input);
        var result = await _dataService.ExecuteQuery<Ccg>(query, parameters);
        return result;
    }
}