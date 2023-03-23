using MediatR;

namespace KidsToyHive.Domain.Models.DomainEvents;

public class DriverCreated : INotification
{
    public DriverCreated(Driver driver)
    {
        Driver = driver;
    }
    public Driver Driver { get; private set; }
}
