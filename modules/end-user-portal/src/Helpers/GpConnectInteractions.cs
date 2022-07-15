using GpConnect.NationalDataSharingPortal.EndUserPortal.Core;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Constants;

public enum GpConnectInteractions
{
    [DisplayParameter("Access Record: HTML")]
    AccessRecordHTML = 1,

    [DisplayParameter("Access Record: Structured")]
    AccessRecordStructured = 2,

    [DisplayParameter("Appointment Management")]
    AppointmentManagement = 3,

    [DisplayParameter("Send Document")]
    SendDocument = 4
}
