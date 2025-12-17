// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Models;
using FluentValidation;
using System;
using KidsToyHive.Core.Features.Locations;
using KidsToyHive.Core.Features.Addresses;
using System.Collections.Generic;
using System.Linq;

namespace KidsToyHive.Core.Features.Customers;

public class CustomerDtoValidator : AbstractValidator<CustomerDto>
{
    public CustomerDtoValidator()
    {
        RuleFor(x => x.CustomerId).NotNull();
        RuleForEach(x => x.CustomerLocations).SetValidator(new CustomerLocationDtoValidator());
    }
}
public class CustomerDto
{
    public Guid CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public AddressDto Address { get; set; }
    public ICollection<CustomerLocationDto> CustomerLocations { get; set; }
    = new HashSet<CustomerLocationDto>();
    public int Version { get; set; }
}
public static class CustomerExtensions
{
    public static CustomerDto ToDto(this Customer customer)
        => new CustomerDto
        {
            CustomerId = customer.CustomerId,
            PhoneNumber = customer.PhoneNumber,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Address = customer.Address.ToDto(),
            Email = customer.Email,
            CustomerLocations = customer.CustomerLocations.Select(x => x.ToDto()).ToList(),
            Version = customer.Version
        };
}

