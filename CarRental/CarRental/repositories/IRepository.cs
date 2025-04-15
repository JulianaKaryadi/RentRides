using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.repositories
{
    public interface IBookingRepository
    {
        DataTable GetPendingReturns(int userId);
        void UpdateBookingReturn(int bookingId, decimal damageCost, decimal totalCost);
    }
}
