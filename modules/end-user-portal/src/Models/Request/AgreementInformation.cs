namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Request;

public partial class AgreementInformation 
{
    public OrganisationInformation? Organisation { get; set; }
    public string UseCase { get; set; } = "";
    public string SoftwareSupplierName { get; set; } = "";
    public GpConnectInteractions? Interactions { get; set; }
    public SignatoryDetails? Signatory { get; set; }

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
}