using gpconnect_user_portal.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace gpconnect_user_portal.Admin.Pages
{
    public partial class UsersModel : BaseModel
    {
        private readonly ILogger<UsersModel> _logger;
        private readonly IAggregateService _aggregateService;
        private readonly IOptionsMonitor<DTO.Response.Configuration.General> _generalOptionsDelegate;

        public UsersModel(ILogger<UsersModel> logger, IAggregateService aggregateService, IOptionsMonitor<DTO.Response.Configuration.General> generalOptionsDelegate) : base(aggregateService, generalOptionsDelegate)
        {
            _logger = logger;
            _aggregateService = aggregateService;
            _generalOptionsDelegate = generalOptionsDelegate;
        }

        public async Task OnGet()
        {
            await RefreshPage();            
        }

        private async Task RefreshPage()
        {
            Users = await _aggregateService.ApplicationService.GetUsers();
        }

        public async Task OnPostSetIsAdmin(int userid, bool makeadmin)
        {
            await _aggregateService.ApplicationService.SetIsAdmin(userid, makeadmin);
            await RefreshPage();
        }
    }
}
