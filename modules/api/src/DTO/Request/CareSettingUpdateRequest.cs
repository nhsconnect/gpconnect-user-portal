namespace GpConnect.NationalDataSharingPortal.Api.Dto.Request
{ 
    public class CareSettingUpdateRequest : BaseChangeRequest
    {
        public int CareSettingId { get; set; }
        public string CareSettingValue { get; set; } = "";
    }
}
