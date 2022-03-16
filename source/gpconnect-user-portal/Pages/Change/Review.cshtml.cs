using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Pages
{
    public partial class ReviewModel : BaseSiteModel
    {
        private readonly ILogger<ReviewModel> _logger;
        private readonly IAggregateService _aggregateService;
        private readonly IOptionsMonitor<DTO.Response.Configuration.General> _generalOptionsDelegate;

        public ReviewModel(ILogger<ReviewModel> logger, IAggregateService aggregateService, IOptionsMonitor<DTO.Response.Configuration.General> generalOptionsDelegate) : base(aggregateService, generalOptionsDelegate)
        {
            _logger = logger;
            _aggregateService = aggregateService;
            _generalOptionsDelegate = generalOptionsDelegate;
        }

        public async Task<IActionResult> OnGetAsync(string siteIdentifier)
        {
            return await PopulateForm(siteIdentifier);
        }

        public async Task<IActionResult> PopulateForm(string siteIdentifier)
        {
            var siteDefinition = await _aggregateService.ApplicationService.GetSiteDefinition(siteIdentifier);
            if (siteDefinition != null)
            {
                SiteIdentifier = siteIdentifier;
                SiteAttributes = siteDefinition.SiteAttributes;
                CanUpdateOrSubmit = siteDefinition.CanUpdateOrSubmit;

                return Page();
            }
            return new NotFoundResult();
        }

        public async Task<IActionResult> OnPostSubmitChangesAsync(string siteIdentifier)
        {
            await _aggregateService.ApplicationService.PostSiteDefinition(siteIdentifier);
            return LocalRedirect($"~/Change/Submitted");
        }
    }
}
