using Henry.Interfaces;
using Henry.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;

namespace Henry.Pages.Boats
{
    public class IndexModel : PageModel
    {
        private IBookingRepository _bookingRepository;
        private IRepairRepository _repairRepository;
        [BindProperty(SupportsGet = true)]
        public int Hours { get; set; }

        public IndexModel(IRepairRepository repairRepository, IBookingRepository bookingRepository)
        {
            _repairRepository = repairRepository;
            _bookingRepository = bookingRepository;
        }

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
        public IActionResult OnGetShowBoth()
        {
            HttpContext.Session.Remove("ViewOnlyAvailable");
            return Page();
        }
        public IActionResult OnGetShowAvailable()
        {
            HttpContext.Session.SetInt32("ViewOnlyAvailable", 1);
            return Page();
        }
        // helper function to determine if a boat should be displayed on the page
        public bool DisplayBoat(string ViewOnlyRepairBoats, string ViewOnlyAvailable, Boat boat, int Hours)
        {
            // if both filter options are on, and they all pass
            if (_repairRepository.HasAnyRepairs(boat.Id) && HttpContext.Session.GetInt32(ViewOnlyRepairBoats) == 1 && HttpContext.Session.GetInt32(ViewOnlyAvailable) == 1 && !_bookingRepository.IsDateTimeBooked(DateTime.Now, DateTime.Now.AddHours(Hours), boat.Id))
            {
                return true;
            }
            // if only the ViewOnlyAvailable filter option is on and passes, but ViewOnlyRepairBoats option is not on
            else if (HttpContext.Session.GetInt32(ViewOnlyRepairBoats) == null && HttpContext.Session.GetInt32(ViewOnlyAvailable) == 1 && !_bookingRepository.IsDateTimeBooked(DateTime.Now, DateTime.Now.AddHours(Hours), boat.Id))
            {
                return true;
            }
            // if only the ViewOnlyRepairBoats filter option is on and passes, but ViewOnlyAvailable option is not on
            else if (_repairRepository.HasAnyRepairs(boat.Id) && HttpContext.Session.GetInt32(ViewOnlyRepairBoats) == 1 && HttpContext.Session.GetInt32(ViewOnlyAvailable) == null)
            {
                return true;
            }
            // if neither filter is on, display everything
            else if (HttpContext.Session.GetInt32(ViewOnlyRepairBoats) == null && HttpContext.Session.GetInt32(ViewOnlyAvailable) == null)
            {
                return true;
            }
            // if its anything else, it dosent pass the check
            else
            {
                return false;
            }
        }
    }
}

