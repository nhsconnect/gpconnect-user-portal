using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

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
            if(!string.IsNullOrEmpty(odsCode))
            {
                PopulateForm(odsCode);
            }
            return Page();
        }

        private void PopulateForm(string odsCode)
        {
            FormOdsCode = odsCode;
        }

        public IActionResult OnPostContinue()
        {
            if (ModelState.IsValid)
            {
                var siteInstanceId = Guid.NewGuid();
                return LocalRedirect($"~/Change/Review/{siteInstanceId}");
            }
            return Page();
        }
    }
}
