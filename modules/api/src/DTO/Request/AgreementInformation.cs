using System.ComponentModel.DataAnnotations;

namespace GpConnect.NationalDataSharingPortal.Api.Dto.Request;

public class AgreementInformation 
{
    public OrganisationInformation Organisation { get; set; }
    [Required]
    public string UseCase { get; set; } = "";
    public string SoftwareSupplierName { get; set; } = "";
    public GpConnectInteractions Interactions { get; set; }
    public SignatoryDetails Signatory { get; set; }

    public class OrganisationInformation
    {
        [Required]
        public string OdsCode { get; set; } = "";
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string AddressLine1 { get; set; } = "";
        
        public string AddressLine2 { get; set; } = "";
        [Required]
        public string Town { get; set; } = "";
        [Required]
        public string County { get; set; } = "";
        [Required]
        public string Country { get; set; } = "";
        [Required]
        public string PostCode { get; set; } = "";
    }

    public class GpConnectInteractions {
        public bool AccessRecordHTMLEnabled { get; set; }
        public bool SendDocumentEnabled { get; set; }
        public bool AppointmentManagementEnabled { get; set; }
        public bool StructuredRecordEnabled { get; set; }
    }

    public class SignatoryDetails
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Position { get; set; }
    }
}