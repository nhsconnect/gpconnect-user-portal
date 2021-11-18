using gpconnect_user_portal.Models;
using gpconnect_user_portal.Models.Interfaces;
using Microsoft.Extensions.Logging;

namespace gpconnect_user_portal.Pages
{
    public class IndexModel : BaseModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, ICommon common) : base(common)
        {
            _logger = logger;            
        }

        public void OnGet()
        {

        }
    }
}
