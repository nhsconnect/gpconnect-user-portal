namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Request;

public partial class AgreementInformation 
{
    public OrganisationInformation? Organisation { get; set; }
    public string UseCase { get; set; } = "";
    public string SoftwareSupplierName { get; set; } = "";
    public GpConnectInteractions? Interactions { get; set; }
    public SignatoryDetails? Signatory { get; set; }
}