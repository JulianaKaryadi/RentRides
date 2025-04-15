using System.Collections.Generic;

namespace CarRental.observer
{
    public class BookingSubject : ISubject
    {
        private List<IObserver> observers = new List<IObserver>();

        public void Attach(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify(int bookingId, int rating, string comment)
        {
            foreach (var observer in observers)
            {
                observer.Update(bookingId, rating, comment);
            }
        }
    }
}
