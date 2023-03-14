using MediatR;
using System;

namespace KidsToyHive.Domain.Models.DomainEvents;

public class CustomerCreated : INotification
{
    public CustomerCreated(Customer customer)
    {
        Customer = customer;
    }
    public Customer Customer { get; private set; }
}
