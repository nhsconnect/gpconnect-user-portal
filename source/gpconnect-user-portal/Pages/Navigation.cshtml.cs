using gpconnect_user_portal.Models;
using gpconnect_user_portal.Models.Interfaces;

namespace gpconnect_appointment_checker.Pages
{
    public class NavigationModel : BaseModel
    {
        public NavigationModel(ICommon common) : base(common)
        {
        }

        public void OnGet()
        {
        }
    }
}
