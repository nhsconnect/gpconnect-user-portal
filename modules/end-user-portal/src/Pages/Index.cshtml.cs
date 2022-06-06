using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages
{
  public class IndexModel : PageModel
  {
    public IActionResult OnGet()
    {
      return Redirect("/Search");
    }
  }
}
