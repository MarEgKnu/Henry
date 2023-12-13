using Henry.Models;

namespace Henry.Interfaces
{
    public interface IBookingRepository
    {

        void AddBooking(BoatBooking booking);

        List<BoatBooking> GetAllBookings();

        bool UpdateBooking(BoatBooking booking);

        bool DeleteBooking(BoatBooking booking);

        BoatBooking GetBooking(int id);

        List<BoatBooking> GetBookingsForBoat(int id);
        bool HasAnyBookings(int id);

        bool IsDateTimeBooked(DateTime time, int boatId);

    }

}
