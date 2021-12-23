using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Pages
{
    public partial class DetailModel : BaseSiteModel
    {
        private readonly ILogger<DetailModel> _logger;
        private readonly IAggregateService _aggregateService;
        private readonly IOptionsMonitor<DTO.Response.Configuration.General> _generalOptionsDelegate;

        public DetailModel(ILogger<DetailModel> logger, IAggregateService aggregateService, IOptionsMonitor<DTO.Response.Configuration.General> generalOptionsDelegate) : base(aggregateService, generalOptionsDelegate)
        {
            _logger = logger;
            _aggregateService = aggregateService;
            _generalOptionsDelegate = generalOptionsDelegate;
        }

        public async Task<IActionResult> OnGetAsync(Guid siteIdentifier)
        {
            await PopulateForm(siteIdentifier);
            ClearModelState();
            return Page();
        }

        public async Task<IActionResult> OnPostLoadSupplierOptionsAsync()
        {
            CanUpdateOrSubmit = true; 
            SkipModelStateForSupplierOptions(true);
            if (ModelState.IsValid)
            {                
                DisplayGpConnectProducts = !string.IsNullOrEmpty(SelectedCareSetting) && !string.IsNullOrEmpty(SelectedSupplier);
                SupplierProducts = await _aggregateService.ReferenceService.GetSupplierProducts(Convert.ToInt16(SelectedSupplier));
            }
            return Page();
        }

        public IActionResult OnPostEnableSupplierUpdate()
        {
            ClearModelState();
            CanUpdateOrSubmit = true;
            DisplayGpConnectProducts = false;
            return Page();
        }

        public async Task<IActionResult> OnPostContinueAsync(IFormCollection formCollection)
        {
            DisplayGpConnectProducts = true;
            if (ModelState.IsValid)
            {   
                var siteDefinition = await _aggregateService.ApplicationService.AddSiteDefinition(formCollection, new DTO.Request.SiteDefinition
                {
                    SiteUniqueIdentifier = SiteIdentifier,
                    SiteOdsCode = FormOdsCode
                });
                return LocalRedirect($"~/Change/Review/{siteDefinition.SiteUniqueIdentifier}");
            }
            return Page();
        }

        private async Task<IActionResult> PopulateForm(Guid siteIdentifier)
        {
            if (siteIdentifier == Guid.Empty)
            {
                CanUpdateOrSubmit = true;
                DisplayGpConnectProducts = false;
                return Page();
            }
            else
            {
                DisplayGpConnectProducts = true;
                var siteDefinition = await _aggregateService.ApplicationService.GetSiteDefinition(siteIdentifier);
                if (siteDefinition != null)
                {
                    CanUpdateOrSubmit = siteDefinition.CanUpdateOrSubmit;
                    SiteAttributes = siteDefinition.SiteAttributes;
                    var selectedSupplier = GetAttributeValue("SelectedSupplier");
                    SupplierProducts = await _aggregateService.ReferenceService.GetSupplierProducts(Convert.ToInt16(selectedSupplier));
                    SiteIdentifier = siteIdentifier;

                    Submitter = GetAttributeValue("Submitter");
                    ContactEmailAddress = GetAttributeValue("ContactEmailAddress");
                    ContactTelephone = GetAttributeValue("ContactTelephone");
                    NoOdsIssued = bool.Parse(GetAttributeValue("NoOdsIssued"));
                    FormOdsCode = GetAttributeValue("FormOdsCode");
                    SiteName = GetAttributeValue("SiteName");
                    SitePostcode = GetAttributeValue("SitePostcode");
                    UseCaseDescription = GetAttributeValue("UseCaseDescription");

                    SelectedCareSetting = GetAttributeValue("SelectedCareSetting");
                    SelectedSupplier = GetAttributeValue("SelectedSupplier");

                    return Page();
                }
                return new NotFoundResult();
            }
        }

        private void ClearModelState()
        {
            ModelState.ClearValidationState("Submitter");
            ModelState.ClearValidationState("ContactEmailAddress");
            ModelState.ClearValidationState("ContactTelephone");
            ModelState.ClearValidationState("FormOdsCode");
            ModelState.ClearValidationState("OdsIssued");
            ModelState.ClearValidationState("SiteName");
            ModelState.ClearValidationState("SitePostcode");
            ModelState.ClearValidationState("RecordAccessHtmlView");
            ModelState.ClearValidationState("RecordAccessStructured");
            ModelState.ClearValidationState("Appointment");
            ModelState.ClearValidationState("UseCaseDescription");
            ModelState.ClearValidationState("SelectedUseCase");
            ModelState.ClearValidationState("SelectedCareSetting");
            ModelState.ClearValidationState("SelectedSupplier");
            ModelState.ClearValidationState("SelectedSupplierProductUseCase");
        }

        private void SkipModelStateForSupplierOptions(bool markFieldAsValid = false)
        {
            ModelState.ClearValidationState("SelectedUseCase");
            ModelState.ClearValidationState("UseCaseDescription");
            ModelState.ClearValidationState("RecordAccessHtmlView");
            ModelState.ClearValidationState("RecordAccessStructured");
            ModelState.ClearValidationState("Appointment");
            ModelState.ClearValidationState("SelectedSupplierProductUseCase");

            if (markFieldAsValid)
            {
                ModelState.MarkFieldValid("SelectedUseCase");
                ModelState.MarkFieldValid("UseCaseDescription");
                ModelState.MarkFieldValid("RecordAccessHtmlView");
                ModelState.MarkFieldValid("RecordAccessStructured");
                ModelState.MarkFieldValid("Appointment");
                ModelState.MarkFieldValid("SelectedSupplierProductUseCase");
            }
        }
    }
}
