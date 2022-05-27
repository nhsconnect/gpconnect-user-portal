using Dapper;
using GpConnect.NationalDataSharingPortal.Api.Dal.Interfaces;
using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using GpConnect.NationalDataSharingPortal.Api.Service.Interface;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace GpConnect.NationalDataSharingPortal.Api.Service;

public class CareSettingService : ICareSettingService
{
    private readonly IDataService _dataService;

    public CareSettingService(IDataService dataService)
    {
        _dataService = dataService;
    }

    public async Task<IEnumerable<CareSetting>> GetCareSettings()
    {
        var query = "reference.get_lookup";
        var parameters = new DynamicParameters();
        parameters.Add("_lookup_type_id", (int)Dal.Enumerations.LookupType.CareSetting, DbType.Int16, ParameterDirection.Input);
        var result = await _dataService.ExecuteQuery<CareSetting>(query, parameters);
        return result;
    }

    public async Task<CareSetting> GetCareSetting(int id)
    {
        var query = "reference.get_lookup_by_id";
        var parameters = new DynamicParameters();
        parameters.Add("_lookup_type_id", (int)Dal.Enumerations.LookupType.CareSetting, DbType.Int16, ParameterDirection.Input);
        parameters.Add("_lookup_id", id, DbType.Int16, ParameterDirection.Input);
        var result = await _dataService.ExecuteQueryFirstOrDefault<CareSetting>(query, parameters);
        return result;
    }

    public async Task<CareSetting> AddCareSetting(CareSettingAddRequest careSettingAddRequest)
    {
        var query = "reference.add_lookup";
        var parameters = new DynamicParameters();
        parameters.Add("_lookup_type_id", (int)Dal.Enumerations.LookupType.CareSetting, DbType.Int16, ParameterDirection.Input);
        parameters.Add("_lookup_value", careSettingAddRequest.CareSettingValue, DbType.String, ParameterDirection.Input);
        parameters.Add("_linked_lookup_id", null, DbType.Int16, ParameterDirection.Input);
        return await _dataService.ExecuteQueryFirstOrDefault<CareSetting>(query, parameters);
    }

    public async Task DisableCareSetting(CareSettingDisableRequest careSettingDisableRequest)
    {
        var query = "reference.enable_disable_lookup";
        var parameters = new DynamicParameters();
        parameters.Add("_lookup_id", careSettingDisableRequest.CareSettingId, DbType.Int16, ParameterDirection.Input);
        parameters.Add("_lookup_type_id", (int)Dal.Enumerations.LookupType.CareSetting, DbType.Int16, ParameterDirection.Input);
        parameters.Add("_is_disabled", careSettingDisableRequest.CareSettingDisabled, DbType.Boolean, ParameterDirection.Input);
        await _dataService.ExecuteQueryFirstOrDefault<CareSetting>(query, parameters);
    }

    public async Task UpdateCareSetting(CareSettingUpdateRequest careSettingUpdateRequest)
    {
        var query = "reference.update_lookup";
        var parameters = new DynamicParameters();
        parameters.Add("_lookup_id", careSettingUpdateRequest.CareSettingId, DbType.Int16, ParameterDirection.Input);
        parameters.Add("_lookup_type_id", (int)Dal.Enumerations.LookupType.CareSetting, DbType.Int16, ParameterDirection.Input);
        parameters.Add("_lookup_value", careSettingUpdateRequest.CareSettingValue, DbType.String, ParameterDirection.Input);
        await _dataService.ExecuteQueryFirstOrDefault<CareSetting>(query, parameters);
    }
}