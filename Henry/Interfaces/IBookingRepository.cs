using Henry.Models;

namespace Henry.Interfaces
{
    public interface IBookingRepository
    {

        public void AddBooking(BoatBooking booking);

        public List<BoatBooking> GetAllBookings();

        public bool UpdateBooking(BoatBooking booking);

        public bool DeleteBooking(BoatBooking booking);

        public BoatBooking GetBooking(int id);
    }
}
