using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GpConnect.NationalDataSharingPortal.Api.Service.Interface
{
    public interface ICareSettingService
    {
        Task<IEnumerable<CareSetting>> GetCareSettings();
        Task<CareSetting> GetCareSetting(int id);
        Task UpdateCareSetting(CareSettingUpdateRequest careSettingUpdateRequest);
        Task EnableDisableCareSetting(CareSettingEnableDisableRequest careSettingEnableDisableRequest);
        Task<CareSetting> AddCareSetting(CareSettingAddRequest careSettingAddRequest);
    }
}