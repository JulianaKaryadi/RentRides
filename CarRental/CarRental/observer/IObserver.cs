using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.observer
{
    public interface IObserver
    {
        void Update(int bookingId, int rating, string comment);
    }
}

