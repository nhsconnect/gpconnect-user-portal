namespace gpconnect_user_portal.Models
{
    public class EndpointRegistration
    {
        public EndpointRegistration()
        {
            EndpointSubmitterDetails = new EndpointSubmitterDetails();
            EndpointSiteDetails = new EndpointSiteDetails();
            EndpointSupplierDetails = new EndpointSupplierDetails();
            EndpointSupplierProductCapability = new EndpointSupplierProductCapability();
            EndpointDataSharingAgreementContactDetails = new EndpointDataSharingAgreementContactDetails();
        }

        public EndpointSubmitterDetails EndpointSubmitterDetails { get; set; }
        public EndpointSiteDetails EndpointSiteDetails { get; set; }
        public EndpointSupplierDetails EndpointSupplierDetails { get; set; }
        public EndpointSupplierProductCapability EndpointSupplierProductCapability { get; set; }
        public EndpointDataSharingAgreementContactDetails EndpointDataSharingAgreementContactDetails { get; set; }

        public bool CanUpdateOrSubmit { get; set; }

        public string SiteUniqueIdentifier { get; set; }

    }
}