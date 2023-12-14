using Henry.Helpers;
using Henry.Interfaces;
using Henry.Models;
using Henry.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Henry.Pages.Boats
{
    public class BookBoatModel : PageModel
    {
        private IMemberRepository _memberRepository;
        private IBoatRepository _boatRepository;
        private IBookingRepository _bookingRepository;

        public string Message { get; set; } = "";

        public SelectList Options { get; set; }

        public Boat BoatToBook { get; set; }
        [BindProperty]
        public BoatBooking Booking { get; set; }
        //[Required(ErrorMessage = "Feltet er krævet")]
        //[BindProperty]
        //[IsNowOrFutureDate(ErrorMessage = "Tid kan ikke være i fortid")]
        //public DateTime BookStart { get; set; }
        //[Required(ErrorMessage = "Feltet er krævet")]
        //[BindProperty]
        //[IsNowOrFutureDate(ErrorMessage = "Tid kan ikke være i fortid")]
        //public DateTime BookEnd { get; set; }
        public BookBoatModel(IMemberRepository memberRepository , IBoatRepository boatRepository, IBookingRepository bookingRepository)
        {
            _memberRepository = memberRepository;
            _boatRepository = boatRepository;
            _bookingRepository = bookingRepository;
        }
        public IActionResult OnGet(int id)
        {
            // redirect to login page if the user is not verified to be logged in currently
            if (!_memberRepository.VerifySession(HttpContext))
            {
                return RedirectToPage("/Login/LogIn");
            }
            else
            {
                // creates a new member list and iterates over GetAllMembers, only adding each member to the list if it is NOT the currently logged in user
                List<Member> members = new List<Member>();
                foreach (var member in _memberRepository.GetAllMembers())
                {
                    if (!(member.UserId == HttpContext.Session.GetInt32("UserId")))
                    {
                        members.Add(member);
                    }
                }
                Options = new SelectList(members, "UserId", "Name");
                BoatToBook = _boatRepository.GetBoat(id);
                return Page();
            }
        }
        public IActionResult OnPost(int id)
        {
            // create selectlist again
            List<Member> members = new List<Member>();
            foreach (var member in _memberRepository.GetAllMembers())
            {
                if (!(member.UserId == HttpContext.Session.GetInt32("UserId")))
                {
                    members.Add(member);
                }
            }
            Options = new SelectList(members, "UserId", "Name");
            BoatToBook = _boatRepository.GetBoat(id);
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // if part of the booking date is already booked by someone else
            else if (_bookingRepository.IsDateTimeBooked(Booking.BookingStart, Booking.BookingEnd ,id))
            {
                Message = "En del af denne tidsperiode er allerede booket af nogle andre";
                return Page();
            }
            // redirect to login page if the user is not verified to be logged in currently
            else if (!_memberRepository.VerifySession(HttpContext))
            {
                return RedirectToPage("/Login/LogIn");
            }
            // if the booking ends at the same time or before the booking starts, display error
            else if (Booking.BookingEnd <= Booking.BookingStart)
            {
                Message = "Sluttiden kan ikke være det samme som, eller før starttid";
                return Page();
            }
            else
            {
                Booking.BoatId = id;
                Booking.UserId = (int)HttpContext.Session.GetInt32("UserId");
                _bookingRepository.AddBooking(Booking);
                return RedirectToPage("/Bookings/Index");
            }
        }
    }
}
