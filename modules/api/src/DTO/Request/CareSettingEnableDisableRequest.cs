namespace GpConnect.NationalDataSharingPortal.Api.Dto.Request;

public class CareSettingEnableDisableRequest : BaseChangeRequest
{
    public int CareSettingId { get; set; }
    public bool CareSettingDisabled { get; set; }
}