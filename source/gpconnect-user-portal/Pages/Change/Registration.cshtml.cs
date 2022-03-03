using gpconnect_user_portal.DTO.Response.Reference;
using gpconnect_user_portal.Helpers;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Pages.Change
{
    public partial class RegistrationModel : BaseSiteModel
    {
        private readonly ILogger<RegistrationModel> _logger;
        private readonly IAggregateService _aggregateService;
        private readonly IOptionsMonitor<DTO.Response.Configuration.General> _generalOptionsDelegate;

        public RegistrationModel(ILogger<RegistrationModel> logger, IAggregateService aggregateService, IOptionsMonitor<DTO.Response.Configuration.General> generalOptionsDelegate) : base(aggregateService, generalOptionsDelegate)
        {
            _logger = logger;
            _aggregateService = aggregateService;
            _generalOptionsDelegate = generalOptionsDelegate;
        }

        public async Task<IActionResult> OnGetAsync(string siteIdentifier)
        {
            EndpointRegistration = new Models.EndpointRegistration();            
            await PopulateForm(siteIdentifier);
            return Page();
        }

        public async Task<IActionResult> OnPostLoadSupplierOptionsAsync()
        {
            await PopulateForm();
            CanUpdateOrSubmit = true;
            SkipModelStateForSupplierOptions(true);
            SkipModelStateForSubmitterDetails(true);
            if (ModelState.IsValid)
            {
                EndpointRegistration.EndpointSupplierDetails.DisplayGpConnectProducts = EndpointRegistration.EndpointSupplierDetails.SelectedCareSetting > 0 && EndpointRegistration.EndpointSupplierDetails.SelectedSupplier > 0;
                EndpointRegistration.EndpointSupplierProductCapability.EnabledSupplierProductCapability = await _aggregateService.ReferenceService.GetSupplierProductCapabilities(EndpointRegistration.EndpointSupplierDetails.SelectedSupplier);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostEnableSupplierUpdate()
        {
            await PopulateForm();
            CanUpdateOrSubmit = true;
            EndpointRegistration.EndpointSupplierDetails.DisplayGpConnectProducts = false;
            return Page();
        }

        public async Task<IActionResult> OnPostContinueAsync()
        {            
            EndpointRegistration.EndpointSupplierProductCapability.EnabledSupplierProductCapability = await _aggregateService.ReferenceService.GetSupplierProductCapabilities(EndpointRegistration.EndpointSupplierDetails.SelectedSupplier);
            ValidateProductCapabilities();
            if (ModelState.IsValid)
            {
                var siteDefinition = await _aggregateService.ApplicationService.AddSiteDefinition(EndpointRegistration.ConvertObjectToJsonData());
                return LocalRedirect($"~/Change/Review/{siteDefinition.SiteUniqueIdentifier}");
            }
            else
            {
                await PopulateForm();
            }
            return Page();
        }

        private void ValidateProductCapabilities()
        {
            if (!EndpointRegistration.EndpointSupplierProductCapability.IsHtmlEnabled)
            {
                ModelState.ClearValidationState("EndpointRegistration.EndpointSupplierProductCapability.RecordAccessHtmlView");
                ModelState.MarkFieldValid("EndpointRegistration.EndpointSupplierProductCapability.RecordAccessHtmlView");
            }
            if (!EndpointRegistration.EndpointSupplierProductCapability.IsStructuredEnabled)
            {
                ModelState.ClearValidationState("EndpointRegistration.EndpointSupplierProductCapability.RecordAccessStructured");
                ModelState.MarkFieldValid("EndpointRegistration.EndpointSupplierProductCapability.RecordAccessStructured");
            }
            if (!EndpointRegistration.EndpointSupplierProductCapability.IsAppointmentEnabled)
            {
                ModelState.ClearValidationState("EndpointRegistration.EndpointSupplierProductCapability.Appointments");
                ModelState.MarkFieldValid("EndpointRegistration.EndpointSupplierProductCapability.Appointments");
            }
            if (!EndpointRegistration.EndpointSupplierProductCapability.IsSendDocumentEnabled)
            {
                ModelState.ClearValidationState("EndpointRegistration.EndpointSupplierProductCapability.SendDocument");
                ModelState.MarkFieldValid("EndpointRegistration.EndpointSupplierProductCapability.SendDocument");
            }
        }

        private async Task<IActionResult> PopulateForm(string siteIdentifier = "")
        {
            EndpointRegistration.EndpointSiteDetails.CCGNames = GetDropDown((int)Services.Enumerations.LookupType.CCGICBName);
            EndpointRegistration.EndpointSupplierDetails.CareSettings = GetDropDown((int)Services.Enumerations.LookupType.CareSetting);
            EndpointRegistration.EndpointSupplierDetails.SupplierProducts = GetProductListWithSupplier(EndpointRegistration.EndpointSupplierDetails.SelectedSupplier);

            if (!string.IsNullOrEmpty(siteIdentifier))
            {
                EndpointRegistration.SiteUniqueIdentifier = siteIdentifier;

                var siteDefinition = await _aggregateService.ApplicationService.GetSiteDefinition(siteIdentifier);
                if (siteDefinition != null)
                {
                    SiteAttributes = siteDefinition.SiteAttributes;

                    EndpointRegistration.EndpointSubmitterDetails.SubmitterContactName = GetAttributeValue("SubmitterContactName");
                    EndpointRegistration.EndpointSubmitterDetails.SubmitterContactEmailAddress = GetAttributeValue("SubmitterContactEmailAddress");
                    EndpointRegistration.EndpointSubmitterDetails.SubmitterContactTelephone = GetAttributeValue("SubmitterContactTelephone");

                    EndpointRegistration.EndpointSiteDetails.CanEditEndpointSiteDetails = false;

                    EndpointRegistration.EndpointSiteDetails.SiteName = GetAttributeValue("SiteName");
                    EndpointRegistration.EndpointSiteDetails.SitePostcode = GetAttributeValue("SitePostcode");
                    EndpointRegistration.EndpointSiteDetails.OdsCode = GetAttributeValue("OdsCode");
                    EndpointRegistration.EndpointSiteDetails.NoOdsIssued = GetAttributeValue("NoOdsIssued").StringToBoolean();
                    EndpointRegistration.EndpointSiteDetails.SelectedCCGName = GetAttributeValue("SelectedCCGName", true);

                    EndpointRegistration.EndpointSupplierDetails.DisplayGpConnectProducts = false;
                    EndpointRegistration.EndpointSupplierDetails.SelectedCareSetting = Convert.ToInt16(GetAttributeValue("SelectedCareSetting"));
                    EndpointRegistration.EndpointSupplierDetails.SelectedSupplier = Convert.ToInt16(GetAttributeValue("SelectedSupplier"));

                    EndpointRegistration.EndpointSupplierProductCapability.EnabledSupplierProductCapability = await _aggregateService.ReferenceService.GetSupplierProductCapabilities(EndpointRegistration.EndpointSupplierDetails.SelectedSupplier);

                    EndpointRegistration.EndpointSupplierProductCapability.RecordAccessHtmlView = GetAttributeValue("RecordAccessHtmlView");
                    EndpointRegistration.EndpointSupplierProductCapability.RecordAccessStructured = GetAttributeValue("RecordAccessStructured");
                    EndpointRegistration.EndpointSupplierProductCapability.Appointments = GetAttributeValue("Appointments");
                    EndpointRegistration.EndpointSupplierProductCapability.SendDocument = GetAttributeValue("SendDocument");
                    EndpointRegistration.EndpointSupplierProductCapability.UseCaseDescription = GetAttributeValue("UseCaseDescription");

                    EndpointRegistration.EndpointDataSharingAgreementContactDetails.DataSharingAgreementConfirmation = GetAttributeValue("DataSharingAgreementConfirmation").StringToBoolean();
                    EndpointRegistration.EndpointDataSharingAgreementContactDetails.DataSharingAgreementContactName = GetAttributeValue("DataSharingAgreementContactName");
                    EndpointRegistration.EndpointDataSharingAgreementContactDetails.DataSharingAgreementContactEmailAddress = GetAttributeValue("DataSharingAgreementContactEmailAddress");
                    EndpointRegistration.EndpointDataSharingAgreementContactDetails.DataSharingAgreementContactTelephone = GetAttributeValue("DataSharingAgreementContactTelephone");
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            else
            {
                EndpointRegistration.EndpointSiteDetails.CanEditEndpointSiteDetails = true;
            }
            return Page();
        }

        private void ClearModelState()
        {
            ModelState.ClearValidationState("EndpointRegistration.EndpointSubmitterDetails.SubmitterContactName");
            ModelState.ClearValidationState("EndpointRegistration.EndpointSubmitterDetails.SubmitterContactEmailAddress");
            ModelState.ClearValidationState("EndpointRegistration.EndpointSubmitterDetails.SubmitterContactTelephone");
            ModelState.ClearValidationState("EndpointRegistration.EndpointSupplierProductCapability.RecordAccessHtmlView");
            ModelState.ClearValidationState("EndpointRegistration.EndpointSupplierProductCapability.RecordAccessStructured");
            ModelState.ClearValidationState("EndpointRegistration.EndpointSupplierProductCapability.Appointments");
            ModelState.ClearValidationState("EndpointRegistration.EndpointSupplierProductCapability.SendDocument");
            ModelState.ClearValidationState("EndpointRegistration.EndpointSupplierProductCapability.UseCaseDescription");
        }

        private void SkipModelStateForSubmitterDetails(bool markFieldAsValid = false)
        {
            ModelState.ClearValidationState("EndpointRegistration.EndpointSubmitterDetails.SubmitterContactName");
            ModelState.ClearValidationState("EndpointRegistration.EndpointSubmitterDetails.SubmitterContactEmailAddress");
            ModelState.ClearValidationState("EndpointRegistration.EndpointSubmitterDetails.SubmitterContactTelephone");

            if (markFieldAsValid)
            {
                ModelState.MarkFieldValid("EndpointRegistration.EndpointSubmitterDetails.SubmitterContactName");
                ModelState.MarkFieldValid("EndpointRegistration.EndpointSubmitterDetails.SubmitterContactEmailAddress");
                ModelState.MarkFieldValid("EndpointRegistration.EndpointSubmitterDetails.SubmitterContactTelephone");
            }
        }

        private void SkipModelStateForSupplierOptions(bool markFieldAsValid = false)
        {
            ModelState.ClearValidationState("EndpointRegistration.EndpointSupplierProductCapability.RecordAccessHtmlView");
            ModelState.ClearValidationState("EndpointRegistration.EndpointSupplierProductCapability.RecordAccessStructured");
            ModelState.ClearValidationState("EndpointRegistration.EndpointSupplierProductCapability.Appointments");
            ModelState.ClearValidationState("EndpointRegistration.EndpointSupplierProductCapability.SendDocument");
            ModelState.ClearValidationState("EndpointRegistration.EndpointSupplierProductCapability.UseCaseDescription");

            if (markFieldAsValid)
            {
                ModelState.MarkFieldValid("EndpointRegistration.EndpointSupplierProductCapability.RecordAccessHtmlView");
                ModelState.MarkFieldValid("EndpointRegistration.EndpointSupplierProductCapability.RecordAccessStructured");
                ModelState.MarkFieldValid("EndpointRegistration.EndpointSupplierProductCapability.Appointments");
                ModelState.MarkFieldValid("EndpointRegistration.EndpointSupplierProductCapability.SendDocument");
                ModelState.MarkFieldValid("EndpointRegistration.EndpointSupplierProductCapability.UseCaseDescription");
            }
        }       

        private IEnumerable<SelectListItem> GetDropDown(int lookupTypeId, int selectedOption = 0)
        {
            var lookup = _aggregateService.ReferenceService.GetLookup(lookupTypeId).Result;
            var options = lookup.Select(option => new SelectListItem() { Text = option.LookupValue, Value = option.LookupId.ToString(), Selected = selectedOption == option.LookupId }).ToList();
            options.Insert(0, new SelectListItem());
            return options;
        }

        private IEnumerable<SelectListItem> GetProductListWithSupplier(int selectedOption = 0)
        {
            var lookup = _aggregateService.ReferenceService.GetProductListWithSupplier().Result;
            var options = lookup.Select(option => new SelectListItem() { Text = option.LookupValue, Value = option.LookupId.ToString(), Selected = selectedOption == option.LookupId }).ToList();
            options.Insert(0, new SelectListItem());
            return options;
        }
    }
}
