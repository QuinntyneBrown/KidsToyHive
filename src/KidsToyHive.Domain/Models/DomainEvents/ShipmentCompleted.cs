using MediatR;
using System;

namespace KidsToyHive.Domain.Models.DomainEvents;

public class ShipmentCompleted : INotification
{
    public Guid ShipmentId { get; set; }
}
