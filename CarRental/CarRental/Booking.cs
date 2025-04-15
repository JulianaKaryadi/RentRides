using System;

namespace CarRental
{
    public partial class Booking
    {
        public int UserId { get; set; }
        public int CarId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalCost { get; set; }
        public decimal Deposit { get; set; }
    }
}

