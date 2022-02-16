using gpconnect_user_portal.Helpers.Constants;
using Microsoft.AspNetCore.Mvc;
using System;

namespace gpconnect_user_portal.Pages.Change
{
    public partial class RegistrationModel : BaseSiteModel
    {
        [BindProperty]
        public Models.EndpointRegistration EndpointRegistration { get; set; }

        //public string OdsCode { get; set; }
        public string PageTitle => SiteIdentifier == Guid.Empty ? DisplayConstants.CHANGEREGISTERTITLE : DisplayConstants.CHANGEUPDATETITLE;

        //[Required(ErrorMessage = MessageConstants.USECASEDESCRIPTIONREQUIREDERRORMESSAGE)]
        //[Display(Name = DisplayConstants.USECASEDESCRIPTIONINPUT)]
        //[BindProperty(SupportsGet = true)]
        //public string UseCaseDescription { get; set; }

        //[Required(ErrorMessage = MessageConstants.SELECTEDCARESETTINGREQUIREDERRORMESSAGE)]
        //[Display(Name = DisplayConstants.CARESETTINGINPUT)]
        //[BindProperty(SupportsGet = true)]
        //public IEnumerable<SelectListItem> CareSettings => GetDropDown(Services.Enumerations.LookupType.CareSetting, GetAttributeValue("SelectedCareSetting"));

        //[Required(ErrorMessage = MessageConstants.SELECTEDSUPPLIERREQUIREDERRORMESSAGE)]
        //[Display(Name = DisplayConstants.SUPPLIERINPUT)]
        //[BindProperty(SupportsGet = true)]
        //public IEnumerable<SelectListItem> Suppliers => GetDropDown(Services.Enumerations.LookupType.Supplier, GetAttributeValue("SelectedSupplier"));

        //[Display(Name = DisplayConstants.CCGICBNAMEIFAPPLICABLE)]
        //[BindProperty(SupportsGet = true)]
        //public IEnumerable<SelectListItem> CCGNames => GetCCGByNames(GetAttributeValue("SelectedCCGName"));

        //[Display(Name = DisplayConstants.CCGICBODSCODEIFAPPLICABLE)]
        //[BindProperty(SupportsGet = true)]
        //public IEnumerable<SelectListItem> CCGOdsCodes => GetCCGByOdsCodes(GetAttributeValue("SelectedCCGOdsCode"));
    }
}