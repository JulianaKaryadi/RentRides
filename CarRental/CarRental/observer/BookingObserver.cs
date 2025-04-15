using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.observer
{
    public class BookingObserver : IObserver
    {
        public void Update(int bookingId, int rating, string comment)
        {
            // Logic to handle the update, e.g., logging, notification, etc.
            Console.WriteLine($"Booking {bookingId} was rated with {rating} stars. Comment: {comment}");
        }
    }
}
