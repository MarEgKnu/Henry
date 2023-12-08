namespace Henry.Models
{
    public class BoatBooking
    {
        public int BookingId { get; set; }

        public int UserId { get; set; }

        public int BoatId { get; set; }

        public DateTime BookingStart { get; set; }

        public DateTime BookingEnd { get; set;}

    }
}
