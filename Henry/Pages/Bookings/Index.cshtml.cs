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
    }
}
