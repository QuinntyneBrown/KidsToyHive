using MediatR;

namespace KidsToyHive.Domain.Models.DomainEvents
{
    public class BookingCreated : INotification
    {
        public BookingCreated(Booking booking)
        {
            Booking = booking;
        }
        public Booking Booking { get; private set; }
    }
}
