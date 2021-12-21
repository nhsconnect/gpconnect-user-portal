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

        public async Task<IActionResult> PopulateForm(string siteDefinitionGuid)
        {
            var siteDefinition = await _aggregateService.ApplicationService.GetSiteDefinition(Guid.Parse(siteDefinitionGuid));
            if (siteDefinition != null)
            {
                SiteAttributes = siteDefinition.SiteAttributes;
                return Page();
            }
            return new NotFoundResult();
        }

        public IActionResult OnPostUpdateChanges()
        {
            return LocalRedirect($"~/Change/Detail/{FormOdsCode}");
        }

        public IActionResult OnPostSubmitChanges()
        {
            return null;
        }
    }
}
