using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

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

        public IActionResult OnGet(Guid id)
        {
            PopulateForm(id);
            return Page();
        }

        private void PopulateForm(Guid siteInstanceId)
        {
            AgreeForDirectCareOnly = false;
            AgreeToDsAgreement = false;
            AgreeToUpdateDPIA = false;
            SiteInstanceId = siteInstanceId;
        }

        public IActionResult OnPostUpdateChanges()
        {
            return LocalRedirect($"~/Change/Detail/{SiteInstanceId}");
        }

        public IActionResult OnPostSubmitChanges()
        {
            return null;
        }
    }
}
