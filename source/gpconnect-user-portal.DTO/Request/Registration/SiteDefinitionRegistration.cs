namespace gpconnect_user_portal.DTO.Request.Registration
{
    public class SiteDefinitionRegistration
    {
        public EndpointSubmitterDetails EndpointSubmitterDetails { get; set; }
        public EndpointSiteDetails EndpointSiteDetails { get; set; }
        public EndpointSupplierDetails EndpointSupplierDetails { get; set; }
        public EndpointSupplierProductCapability EndpointSupplierProductCapability { get; set; }
        public EndpointDataSharingAgreementContactDetails EndpointDataSharingAgreementContactDetails { get; set; }
        public string SiteUniqueIdentifier { get; set; }
    }
}
