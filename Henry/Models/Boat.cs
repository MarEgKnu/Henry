using Henry.Interfaces;
using Henry.Services;
using System.ComponentModel.DataAnnotations;

namespace Henry.Models
{
    public enum BoatType
    {
        Jolle,
        Sejlskib,
        Motorbåd
    }
    public class Boat
    {
        public int Id { get; set; }

        [Display(Name = "Navn")]
        [Required(ErrorMessage = "Navn er krævet")]
        public string? Name { get; set; }
        [Display(Name = "Beskrivelse")]
        [Required(ErrorMessage = "Beskrivelse er krævet")]
        public string? Description { get; set; }

        public string? Img { get; set; }
        [Display(Name = "Oprettet")]
        public DateTime Created {  get; set; }
        [Display(Name = "Bådtype")]
        [Required(ErrorMessage = "Bådtype er krævet")]
        public BoatType? Type { get; set; }
        /// <summary>
        /// Returns true if the boat is available right now (ie not booked), and false if it is not
        /// </summary>
        public bool IsAvailable 
        { 
            get
            {
                IBookingRepository bookingRepository = new BookingRepository();
                foreach (var booking in bookingRepository.GetBookingsForBoat(Id))
                {
                    if (booking.BookingStart <= DateTime.Now)
                    {
                        return false;
                    }
                    
                }
                return true;
            } 
        }
        /// <summary>
        /// Returns the booking for the boat at DateTime.Now, or null if not booked
        /// </summary>
        public BoatBooking? CurrentBooking 
        {
            get
            {
                IBookingRepository bookingRepo = new BookingRepository();
                foreach (var booking in bookingRepo.GetBookingsForBoat(Id))
                {
                    if (booking.BookingStart <= DateTime.Now)
                    {
                        return booking;
                    }
                }
                return null;
            }
        }

        public override string ToString()
        {
            return $"Navn: {Name}\n" +
                   $"ID: {Id.ToString()}\n" +
                   $"Beskrivelse: {Description}\n" +
                   $"Oprettet: {Created}\n" +
                   $"Bådtype: {Type}";
        }
    }
}
