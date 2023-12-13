using Henry.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Henry.Pages.Bookings
{
    public class IndexModel : PageModel
    {
        private IBookingRepository _bookingRepository;
        private IMemberRepository _memberRepository;

        public IndexModel(IBookingRepository bookingRepository, IMemberRepository memberRepository)
        {
            _bookingRepository = bookingRepository;
            _memberRepository = memberRepository;
        }
        public IActionResult OnGet()
        {
            // verify the user is logged in
            if (!_memberRepository.VerifySession(HttpContext))
            {
                return RedirectToPage("/LogIn/LogIn");
            }
            else
            {
                return Page();
            }

        }
        public IActionResult OnGetAdmin()
        {
            // verify the user is logged in and an administrator
            if (!_memberRepository.VerifySessionAdmin(HttpContext))
            {
                return RedirectToPage("/LogIn/LogInNeedAdmin");
            }
            else
            {
                HttpContext.Session.SetInt32("AdminViewBookings", 1);
                return Page();
            }

        }
        public IActionResult OnGetRemoveAdmin()
        {
            // verify the user is logged in and an administrator
            if (!_memberRepository.VerifySessionAdmin(HttpContext))
            {
                return RedirectToPage("/LogIn/LogInNeedAdmin");
            }
            else
            {
                HttpContext.Session.Remove("AdminViewBookings");
                return Page();
            }

        }
    }
}
