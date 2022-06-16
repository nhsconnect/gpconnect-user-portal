using System;

namespace GpConnect.NationalDataSharingPortal.Api.Dto.Response;

public class TransparencySite
{
    public Guid Id { get; set; }
    public string OdsCode { get; set; } = "";
    public string Name { get; set; } = "";
    public string Line1 { get; set; } = "";
    public string Line2 { get; set; } = "";
    public string Town { get; set; } = "";
    public string County { get; set; } = "";
    public string Country { get; set; } = "";
    public string Postcode { get; set; } = "";

    public bool AccessRecordHTMLEnabled { get; set; }
    public bool StructuredRecordEnabled { get; set; }
    public bool AppointmentManagementEnabled { get; set; }
    public bool SendDocumentEnabled { get; set; }

    public string CcgIcbName { get; set; } = "";
    public string CcgIcbOdsCode { get; set; } = "";

    public string UseCase { get; set; } = "";
}
