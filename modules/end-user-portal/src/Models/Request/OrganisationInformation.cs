namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Request;

public class OrganisationInformation
{   
    public string OdsCode { get; set; } = "";
    public string Name { get; set; } = "";
    public string AddressLine1 { get; set; } = "";
    public string AddressLine2 { get; set; } = "";
    public string Town { get; set; } = "";
    public string County { get; set; } = "";
    public string Country { get; set; } = "";
    public string PostCode { get; set; } = "";
}
