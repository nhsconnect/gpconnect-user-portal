using gpconnect_user_portal.DTO.Response.Reference;
using gpconnect_user_portal.Helpers;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
        private readonly List<Organisation> _organisationList;

        public RegistrationModel(ILogger<RegistrationModel> logger, IAggregateService aggregateService, IOptionsMonitor<DTO.Response.Configuration.General> generalOptionsDelegate) : base(aggregateService, generalOptionsDelegate)
        {
            _logger = logger;
            _aggregateService = aggregateService;
            _generalOptionsDelegate = generalOptionsDelegate;
            _organisationList = _aggregateService.ReferenceService.GetOrganisations().Result;
        }

        public IActionResult OnGet()
        {
            EndpointRegistration = new Models.EndpointRegistration();
            PopulateForm();
            return Page();
        }

        public async Task<IActionResult> OnPostLoadSupplierOptionsAsync()
        {
            PopulateForm();
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

        public IActionResult OnPostEnableSupplierUpdate()
        {
            PopulateForm();
            CanUpdateOrSubmit = true;
            EndpointRegistration.EndpointSupplierDetails.DisplayGpConnectProducts = false;
            return Page();
        }

        public async Task<IActionResult> OnPostContinueAsync()
        {
            PopulateForm();
            EndpointRegistration.EndpointSupplierProductCapability.EnabledSupplierProductCapability = await _aggregateService.ReferenceService.GetSupplierProductCapabilities(EndpointRegistration.EndpointSupplierDetails.SelectedSupplier);
            ValidateProductCapabilities();
            if (ModelState.IsValid)
            {
                var siteDefinition = await _aggregateService.ApplicationService.AddSiteDefinition(EndpointRegistration.ConvertObjectToJsonData());
                return LocalRedirect($"~/Change/Review/{siteDefinition.SiteUniqueIdentifier}");
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
                ModelState.ClearValidationState("EndpointRegistration.EndpointSupplierProductCapability.Appointment");
                ModelState.MarkFieldValid("EndpointRegistration.EndpointSupplierProductCapability.Appointment");
            }
            if (!EndpointRegistration.EndpointSupplierProductCapability.IsSendDocumentEnabled)
            {
                ModelState.ClearValidationState("EndpointRegistration.EndpointSupplierProductCapability.SendDocument");
                ModelState.MarkFieldValid("EndpointRegistration.EndpointSupplierProductCapability.SendDocument");
            }
        }

        private void PopulateForm()
        {
            EndpointRegistration.EndpointSiteDetails.CCGNames = GetCCGByNames();
            EndpointRegistration.EndpointSiteDetails.CCGOdsCodes = GetCCGByOdsCodes();
            EndpointRegistration.EndpointSupplierDetails.CareSettings = GetDropDown(Services.Enumerations.LookupType.CareSetting);
            EndpointRegistration.EndpointSupplierDetails.SupplierProducts = GetProductListWithSupplier(EndpointRegistration.EndpointSupplierDetails.SelectedSupplier);
        }

        private void ClearModelState()
        {
            ModelState.ClearValidationState("EndpointRegistration.EndpointSubmitterDetails.SubmitterContactName");
            ModelState.ClearValidationState("EndpointRegistration.EndpointSubmitterDetails.SubmitterContactEmailAddress");
            ModelState.ClearValidationState("EndpointRegistration.EndpointSubmitterDetails.SubmitterContactTelephone");
            ModelState.ClearValidationState("FormOdsCode");
            ModelState.ClearValidationState("OdsIssued");
            ModelState.ClearValidationState("SiteName");
            ModelState.ClearValidationState("SitePostcode");
            ModelState.ClearValidationState("EndpointRegistration.EndpointSupplierProductCapability.RecordAccessHtmlView");
            ModelState.ClearValidationState("EndpointRegistration.EndpointSupplierProductCapability.RecordAccessStructured");
            ModelState.ClearValidationState("EndpointRegistration.EndpointSupplierProductCapability.Appointment");
            ModelState.ClearValidationState("EndpointRegistration.EndpointSupplierProductCapability.SendDocument");
            ModelState.ClearValidationState("UseCaseDescription");
            ModelState.ClearValidationState("SelectedUseCase");
            ModelState.ClearValidationState("SelectedCareSetting");
            ModelState.ClearValidationState("SelectedSupplier");
            ModelState.ClearValidationState("SelectedSupplierProductUseCase");
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
            //ModelState.ClearValidationState("SelectedUseCase");
            //ModelState.ClearValidationState("UseCaseDescription");
            ModelState.ClearValidationState("EndpointRegistration.EndpointSupplierProductCapability.RecordAccessHtmlView");
            ModelState.ClearValidationState("EndpointRegistration.EndpointSupplierProductCapability.RecordAccessStructured");
            ModelState.ClearValidationState("EndpointRegistration.EndpointSupplierProductCapability.Appointment");
            ModelState.ClearValidationState("EndpointRegistration.EndpointSupplierProductCapability.SendDocument");
            //ModelState.ClearValidationState("SelectedSupplierProductUseCase");

            if (markFieldAsValid)
            {
                //ModelState.MarkFieldValid("SelectedUseCase");
                //ModelState.MarkFieldValid("UseCaseDescription");
                ModelState.MarkFieldValid("EndpointRegistration.EndpointSupplierProductCapability.RecordAccessHtmlView");
                ModelState.MarkFieldValid("EndpointRegistration.EndpointSupplierProductCapability.RecordAccessStructured");
                ModelState.MarkFieldValid("EndpointRegistration.EndpointSupplierProductCapability.Appointment");
                ModelState.MarkFieldValid("EndpointRegistration.EndpointSupplierProductCapability.SendDocument");
                //ModelState.MarkFieldValid("SelectedSupplierProductUseCase");
            }
        }

        private IEnumerable<SelectListItem> GetCCGByNames(int selectedCCGName = 0)
        {
            var options = _organisationList.OrderBy(x => x.Name).Select(option => new SelectListItem() { Text = option.Name, Value = option.OrganisationId.ToString(), Selected = selectedCCGName == option.OrganisationId }).ToList();
            options.Insert(0, new SelectListItem());
            return options;
        }

        private IEnumerable<SelectListItem> GetCCGByOdsCodes(int selectedCCGCode = 0)
        {
            var options = _organisationList.OrderBy(x => x.OdsCode).Select(option => new SelectListItem() { Text = option.OdsCode, Value = option.OrganisationId.ToString(), Selected = selectedCCGCode == option.OrganisationId }).ToList();
            options.Insert(0, new SelectListItem());
            return options;
        }        

        private IEnumerable<SelectListItem> GetDropDown(Services.Enumerations.LookupType lookupType, int selectedOption = 0)
        {
            var lookup = _aggregateService.ReferenceService.GetLookup(lookupType).Result;
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
