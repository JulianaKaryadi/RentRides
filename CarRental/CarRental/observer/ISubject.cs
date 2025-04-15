using System.Collections.Generic;

namespace CarRental.observer
{
    public interface ISubject
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify(int bookingId, int rating, string comment);
    }
}
