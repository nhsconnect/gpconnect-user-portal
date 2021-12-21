using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IActionResult OnGet(string odsCode)
        {
            if (!string.IsNullOrEmpty(odsCode))
            {
                PopulateForm(odsCode);
            }
            ClearModelState();
            return Page();
        }

        public async Task<IActionResult> OnPostLoadSupplierOptionsAsync()
        {
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
            DisplayGpConnectProducts = false;
            return Page();
        }

        public async Task<IActionResult> OnPostContinueAsync(IFormCollection collection)
        {
            DisplayGpConnectProducts = true;
            if (ModelState.IsValid)
            {                
                var siteAttributes = new List<DTO.Request.SiteAttribute>();
                siteAttributes.AddRange(
                    collection.Select(x => new DTO.Request.SiteAttribute { SiteAttributeName = x.Key, SiteAttributeValue = x.Value }).ToList()
                );

                var siteDefinition = await _aggregateService.ApplicationService.AddSiteDefinition(new DTO.Request.SiteDefinition
                {
                    SiteOdsCode = FormOdsCode,
                    SiteAttributes = siteAttributes
                });
                return LocalRedirect($"~/Change/Review/{siteDefinition.SiteUniqueIdentifier}");
            }
            return Page();
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

        private void PopulateForm(string odsCode)
        {
            OdsCode = odsCode;
            FormOdsCode = odsCode;
        }
    }
}
