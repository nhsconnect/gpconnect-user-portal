using gpconnect_user_portal.Models.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace gpconnect_user_portal.Models
{
    public abstract class BaseModel : PageModel
    {
        protected BaseModel(ICommon common) { }
    }
}