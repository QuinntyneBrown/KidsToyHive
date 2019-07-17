using KidsToyHive.Domain.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Domain.Features.Customers
{
    public class CustomerDtoValidator: AbstractValidator<CustomerDto>
    {
        public CustomerDtoValidator()
        {
            RuleFor(x => x.CustomerId).NotNull();
        }
    }

    public class CustomerDto
    {        
        public Guid CustomerId { get; set; }
        public int Version { get; set; }
    }

    public static class CustomerExtensions
    {        
        public static CustomerDto ToDto(this Customer customer)
            => new CustomerDto
            {
                CustomerId = customer.CustomerId,
                Version = customer.Version
            };
    }
}
