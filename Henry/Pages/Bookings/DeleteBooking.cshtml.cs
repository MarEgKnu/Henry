using Henry.Interfaces;
using Henry.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Henry.Pages.Bookings
{
    public class DeleteBookingModel : PageModel
    {
        private IMemberRepository _memberRepository;
        private IBookingRepository _bookingRepository;
        private IBoatRepository _boatRepository;

        public BoatBooking BookingToDelete { get; set; }
        public DeleteBookingModel(IMemberRepository memberRepository, IBookingRepository bookingRepository, IBoatRepository boatRepository)
        {
            _memberRepository = memberRepository;
            _bookingRepository = bookingRepository;
            _boatRepository = boatRepository;

        }
        public IActionResult OnGet(int id)
        {
            // checks the user is a verified user upon entering
            if (!_memberRepository.VerifySession(HttpContext))
            {
                return RedirectToPage("/LogIn/LogIn");
            }
            BookingToDelete = _bookingRepository.GetBooking(id);
            // checks if the user currently logged in is the "Owner" of the booking, if so no admin rights are required
            if ((_bookingRepository.GetBooking(id).UserId == HttpContext.Session.GetInt32("UserId")))
            {
                return Page();
            }
            // if the user is NOT the owner of the booking, admin rights are required to delete
            else
            {
                // checks if the user has have admin rights to edit
                if (_memberRepository.VerifySessionAdmin(HttpContext))
                {
                    return Page();
                }
                else
                {
                    return RedirectToPage("/LogIn/LogInNeedAdmin");
                }
            }
        }
        public IActionResult OnPost(int id)
        {
            // checks the user is a verified user upon entering
            if (!_memberRepository.VerifySession(HttpContext))
            {
                return RedirectToPage("/LogIn/LogIn");
            }
            BookingToDelete = _bookingRepository.GetBooking(id);
            // checks if the user currently logged in is the "Owner" of the booking, if so no admin rights are required
            if ((_bookingRepository.GetBooking(id).UserId == HttpContext.Session.GetInt32("UserId")))
            {
                _bookingRepository.DeleteBooking(_bookingRepository.GetBooking(id));
                return RedirectToPage("Index");
            }
            // if the user is NOT the owner of the booking, admin rights are required to delete
            else
            {
                // checks if the user has have admin rights to edit
                if (_memberRepository.VerifySessionAdmin(HttpContext))
                {
                    _bookingRepository.DeleteBooking(_bookingRepository.GetBooking(id));
                    return RedirectToPage("Index");
                }
                else
                {
                    return RedirectToPage("/LogIn/LogInNeedAdmin");
                }
            }
        }


    }
}
