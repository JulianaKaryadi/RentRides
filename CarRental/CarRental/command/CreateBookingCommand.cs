using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.command
{
    public class CreateBookingCommand : ICommand
    {
        private readonly BookingService _service;
        private readonly Booking _booking;

        public CreateBookingCommand(BookingService service, Booking booking)
        {
            _service = service;
            _booking = booking;
        }

        public void Execute()
        {
            _service.CreateBooking(_booking);
        }
    }
}
