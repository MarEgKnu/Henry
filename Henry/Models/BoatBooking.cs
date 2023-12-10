using Henry.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Henry.Models
{
    public class BoatBooking
    {
        public int BookingId { get; set; }

        public int UserId { get; set; }

        public int BoatId { get; set; }

        [Required(ErrorMessage = "Feltet er krævet")]
        [BindProperty]
        [IsNowOrFutureDate(ErrorMessage = "Tid kan ikke være i fortid")]

        public DateTime BookingStart { get; set; }

        [Required(ErrorMessage = "Feltet er krævet")]
        [BindProperty]
        [IsNowOrFutureDate(ErrorMessage = "Tid kan ikke være i fortid")]
        public DateTime BookingEnd { get; set;}

    }
}
