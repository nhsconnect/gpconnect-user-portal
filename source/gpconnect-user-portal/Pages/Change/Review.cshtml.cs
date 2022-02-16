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

        public async Task<IActionResult> OnGetAsync(string id)
        {
            return await PopulateForm(id);
        }

        public async Task<IActionResult> PopulateForm(string siteDefinitionIdentifier)
        {
            var siteDefinition = await _aggregateService.ApplicationService.GetSiteDefinition(siteDefinitionIdentifier);
            if (siteDefinition != null)
            {
                SiteIdentifier = Guid.Parse(siteDefinitionIdentifier);
                SiteAttributes = siteDefinition.SiteAttributes;
                CanUpdateOrSubmit = siteDefinition.CanUpdateOrSubmit;

                return Page();
            }
            return new NotFoundResult();
        }

        public IActionResult OnPostUpdateChanges()
        {
            return LocalRedirect($"~/Change/Detail/{SiteIdentifier}");
        }

        public async Task<IActionResult> OnPostSubmitChangesAsync()
        {
            await _aggregateService.ApplicationService.PostSiteDefinition(SiteIdentifier);
            return LocalRedirect($"~/Change/Submitted");
        }
    }
}
