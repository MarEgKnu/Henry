using Henry.Helpers;
using Henry.Interfaces;
using Henry.Models;
using System.Runtime.CompilerServices;

namespace Henry.Services
{
    public class BookingRepository : IBookingRepository
    {

        private string jsonFileName = @"Data\BookingData.json";


        /// <summary>
        /// Adds the given booking param to the repository, and gives it a new unique ID
        /// </summary>
        /// <param name="booking"></param>
        public void AddBooking(BoatBooking booking)
        {
            List<int> BookingIds = new List<int>();

            List<BoatBooking> bookings = GetAllBookings();

            foreach (var aBooking in bookings)
            {
                BookingIds.Add(aBooking.BookingId);
            }
            if (BookingIds.Count != 0)
            {
                int start = BookingIds.Max();
                booking.BookingId = start + 1;
            }
            else
            {
                booking.BookingId = 1;
            }
            bookings.Add(booking);
            JsonFileWriter<BoatBooking>.WriteToJson(bookings, jsonFileName);
        }
        /// <summary>
        /// Deletes a booking with the same ID as the provided param from the repository
        /// </summary>
        /// <param name="booking"></param>
        /// <returns>bool</returns>
        public bool DeleteBooking(BoatBooking booking)
        {
            bool sucess;
            // checks if it deleted anything, if not return false
            List<BoatBooking> bookings = GetAllBookings();
            foreach (BoatBooking b in bookings)
            {
                if (b.BookingId == booking.BookingId)
                {
                    sucess = bookings.Remove(b);             
                    JsonFileWriter<BoatBooking>.WriteToJson(bookings, jsonFileName);
                    return sucess;
                }
            }
            return false;
        }
        /// <summary>
        /// Gets all bookings currently in the repository
        /// </summary>
        /// <returns>A list of bookings</returns>
        public List<BoatBooking> GetAllBookings()
        {
            return JsonFileReader<BoatBooking>.ReadJson(jsonFileName);
        }
        /// <summary>
        /// Returns the booking with an id matching the param. A empty booking object is returned if none is found
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A boat object if found, or empty booking obj if none found</returns>
        public BoatBooking GetBooking(int id)
        {
            foreach (var booking in GetAllBookings())
            {
                if (booking.BookingId == id)
                {
                    return booking;
                }
            }
            return new BoatBooking();
        }
        /// <summary>
        /// Updates the booking with the same ID as the given param and overwrites all relavant attributes
        /// </summary>
        /// <param name="booking"></param>
        /// <returns>True if sucessfull, false if not</returns>
        public bool UpdateBooking(BoatBooking booking)
        {
            if (booking != null)
            {
                List<BoatBooking> bookings = GetAllBookings();
                foreach (var bo in bookings)
                {
                    if (bo.BookingId == booking.BookingId)
                    {
                        // update more properties maybe?
                        bo.BookingEnd = booking.BookingEnd;
                        JsonFileWriter<BoatBooking>.WriteToJson(bookings, jsonFileName);
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Returns a list of BoatBookings for the inputted boatId
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A list of BoatBooking objects</returns>
        public List<BoatBooking> GetBookingsForBoat(int id)
        {
            List<BoatBooking> bookings = new List<BoatBooking>();
            foreach (BoatBooking booking in GetAllBookings())
            {
                if (booking.BoatId == id)
                {
                    bookings.Add(booking);
                }
            }   
            // sort this properly so it lists the early bookings first
            return bookings;
        }
        /// <summary>
        /// Checks if the inputted boatId has any bookings attached to it
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if 1 or more bookings exist, false if not</returns>
        public bool HasAnyBookings(int id)
        {
            foreach(BoatBooking booking in GetAllBookings())
            {
                if (booking.BoatId == id)
                {
                    return true;
                }
            }
            return false;
        }
        //public bool IsDateTimeBooked(DateTime time, int boatId)
        //{
        //    foreach(var booking in GetAllBookings())
        //    {
        //        if (booking.BoatId == boatId)
        //        {
        //            if (time >= booking.BookingStart && time <= booking.BookingEnd)
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}

        public bool IsDateTimeBooked(DateTime timeStart, DateTime timeEnd, int boatId)
        {
            foreach (var booking in GetAllBookings())
            {
                if (booking.BoatId == boatId)
                {
                    // if the inputted startTime is bigger than the BookingStart, aswell as smaller than the bookingEnd
                    if (timeStart >= booking.BookingStart && timeStart <= booking.BookingEnd)
                    {
                        return true;
                    }
                    // if the inputted endTime is bigger than the BookingStart, aswell as smaller than the bookingEnd
                    else if (timeEnd >= booking.BookingStart && timeEnd <= booking.BookingEnd)
                    {
                        return true;
                    }
                    else if (timeStart <= booking.BookingStart && timeEnd >= booking.BookingEnd)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
