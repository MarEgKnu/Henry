using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Henry.Pages.Boats
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
        public IActionResult OnGetShowAll()
        {
            HttpContext.Session.Remove("ViewOnlyRepairBoats");
            return Page();

        }
        public IActionResult OnGetShowRepair()
        {
            HttpContext.Session.SetInt32("ViewOnlyRepairBoats", 1);
            return Page();

        }
    }
}
