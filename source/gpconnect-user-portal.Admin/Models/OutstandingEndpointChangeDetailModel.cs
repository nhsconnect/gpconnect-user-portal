using gpconnect_user_portal.DAL.Resources;
using gpconnect_user_portal.Helpers;
using Microsoft.AspNetCore.Html;
using System.ComponentModel.DataAnnotations;

namespace gpconnect_user_portal.Admin.Pages
{
    public partial class OutstandingEndpointChangeDetailModel : BaseSiteModel
    {        
        [Display(Name = "DataSharingAgreementContactName", ResourceType = typeof(DataFieldNameResources))]
        public string DataSharingAgreementContactName => GetAttributeValue("DataSharingAgreementContactName");

        [Display(Name = "DataSharingAgreementContactEmailAddress", ResourceType = typeof(DataFieldNameResources))]
        public string DataSharingAgreementContactEmailAddress => GetAttributeValue("DataSharingAgreementContactEmailAddress");

        [Display(Name = "DataSharingAgreementContactTelephone", ResourceType = typeof(DataFieldNameResources))]
        public string DataSharingAgreementContactTelephone => GetAttributeValue("DataSharingAgreementContactTelephone");

        [Display(Name = "DataSharingAgreementConfirmation", ResourceType = typeof(DataFieldNameResources))]
        public HtmlString DataSharingAgreementConfirmation => GetAttributeValue("DataSharingAgreementConfirmation").StringToBooleanHtml();

        [Display(Name = "SiteName", ResourceType = typeof(DataFieldNameResources))]
        public string SiteName => GetAttributeValue("SiteName");

        [Display(Name = "OdsCode", ResourceType = typeof(DataFieldNameResources))]
        public string OdsCode => GetAttributeValue("OdsCode");

        [Display(Name = "NoOdsIssued", ResourceType = typeof(DataFieldNameResources))]
        public HtmlString NoOdsIssued => GetAttributeValue("NoOdsIssued").StringToBooleanHtml();

        [Display(Name = "SitePostcode", ResourceType = typeof(DataFieldNameResources))]
        public string SitePostcode => GetAttributeValue("SitePostcode");

        [Display(Name = "SelectedCCGNameIfApplicable", ResourceType = typeof(DataFieldNameResources))]
        public string SelectedCCGName => GetAttributeValue("SelectedCCGName", true);

        [Display(Name = "SelectedSupplierProduct", ResourceType = typeof(DataFieldNameResources))]
        public string SelectedSupplier => GetAttributeValue("SelectedSupplier", true);

        [Display(Name = "SelectedCareSetting", ResourceType = typeof(DataFieldNameResources))]
        public string SelectedCareSetting => GetAttributeValue("SelectedCareSetting", true);

        [Display(Name = "RecordAccessHtmlView", ResourceType = typeof(DataFieldNameResources))]
        public string RecordAccessHtmlView => GetAttributeValue("RecordAccessHtmlView");

        [Display(Name = "RecordAccessStructured", ResourceType = typeof(DataFieldNameResources))]
        public string RecordAccessStructured => GetAttributeValue("RecordAccessStructured");

        [Display(Name = "Appointments", ResourceType = typeof(DataFieldNameResources))]
        public string Appointments => GetAttributeValue("Appointments");

        [Display(Name = "SendDocument", ResourceType = typeof(DataFieldNameResources))]
        public string SendDocument => GetAttributeValue("SendDocument");

        [Display(Name = "UseCaseDescription", ResourceType = typeof(DataFieldNameResources))]
        public string UseCaseDescription => GetAttributeValue("UseCaseDescription");

        [Display(Name = "SubmitterContactName", ResourceType = typeof(DataFieldNameResources))]
        public string SubmitterContactName => GetAttributeValue("SubmitterContactName");

        [Display(Name = "SubmitterContactEmailAddress", ResourceType = typeof(DataFieldNameResources))]
        public string SubmitterContactEmailAddress => GetAttributeValue("SubmitterContactEmailAddress");

        [Display(Name = "SubmitterContactTelephone", ResourceType = typeof(DataFieldNameResources))]
        public string SubmitterContactTelephone => GetAttributeValue("SubmitterContactTelephone");
    }
}