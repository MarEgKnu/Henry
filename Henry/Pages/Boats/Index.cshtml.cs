using Henry.Helpers;
using Henry.Interfaces;
using Henry.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Henry.Pages.Boats
{
    public class IndexModel : PageModel
    {
        private IBookingRepository _bookingRepository;
        private IRepairRepository _repairRepository;
        [BindProperty(SupportsGet = true)]
        public int Hours { get; set; }
        [BindProperty(SupportsGet = true)]
        [Required(ErrorMessage = "Dato er krævet")]
        [IsNowOrFutureDate(ErrorMessage = "Dato kan ikke være i fortid")]
        public DateTime Date { get; set; }

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
            if (!ModelState.IsValid)
            {
                return Page();
            }
            HttpContext.Session.SetInt32("ViewOnlyAvailable", 1);
            return Page();
        }
        // helper function to determine if a boat should be displayed on the page
        //public bool DisplayBoat(string ViewOnlyRepairBoats, string ViewOnlyAvailable, Boat boat, int Hours, DateTime date)
        //{
        //    // if both filter options are on, and they all pass
        //    if (_repairRepository.HasAnyRepairs(boat.Id) && HttpContext.Session.GetInt32(ViewOnlyRepairBoats) == 1 && HttpContext.Session.GetInt32(ViewOnlyAvailable) == 1 && !_bookingRepository.IsDateTimeBooked(date, date.AddHours(Hours), boat.Id))
        //    {
        //        return true;
        //    }
        //    // if only the ViewOnlyAvailable filter option is on and passes, but ViewOnlyRepairBoats option is not on
        //    else if (HttpContext.Session.GetInt32(ViewOnlyRepairBoats) == null && HttpContext.Session.GetInt32(ViewOnlyAvailable) == 1 && !_bookingRepository.IsDateTimeBooked(date, date.AddHours(Hours), boat.Id))
        //    {
        //        return true;
        //    }
        //    // if only the ViewOnlyRepairBoats filter option is on and passes, but ViewOnlyAvailable option is not on
        //    else if (_repairRepository.HasAnyRepairs(boat.Id) && HttpContext.Session.GetInt32(ViewOnlyRepairBoats) == 1 && HttpContext.Session.GetInt32(ViewOnlyAvailable) == null)
        //    {
        //        return true;
        //    }
        //    // if neither filter is on, display everything
        //    else if (HttpContext.Session.GetInt32(ViewOnlyRepairBoats) == null && HttpContext.Session.GetInt32(ViewOnlyAvailable) == null)
        //    {
        //        return true;
        //    }
        //    // if its anything else, it dosent pass the check
        //    else
        //    {
        //        return false;
        //    }
        //}

        // new refactored helper func
        public bool DisplayBoat(string ViewOnlyRepairBoats, string ViewOnlyAvailable, Boat boat, int Hours, DateTime date)
        {
            // if the boat does not have any repairs, and the user only want to view boats with repairs, skip it
            if (!_repairRepository.HasAnyRepairs(boat.Id) && HttpContext.Session.GetInt32(ViewOnlyRepairBoats) == 1)
            {
                return false;
            }
            // if the user wants to only see available boats, and the boat has a booking at the specified date, skip it
            else if (HttpContext.Session.GetInt32(ViewOnlyAvailable) == 1 && _bookingRepository.IsDateTimeBooked(date, date.AddHours(Hours), boat.Id))
            {

                return false;
            }
            // if all checks passes, display the boat to the user
            else
            {
                return true;
            }
        }
    }
}

