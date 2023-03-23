// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Models;
using FluentValidation;
using System;
using KidsToyHive.Core.Features.Locations;

namespace KidsToyHive.Core.Features.Customers;

public class CustomerLocationDtoValidator : AbstractValidator<CustomerLocationDto>
{
    public CustomerLocationDtoValidator()
    {
        RuleFor(x => x.CustomerLocationId).NotNull();
        RuleFor(x => x.Name).NotNull();
    }
}
public class CustomerLocationDto
{
    public Guid CustomerLocationId { get; set; }
    public string Name { get; set; }
    public Guid? LocationId { get; set; }
    public LocationDto Location { get; set; }
    public int Version { get; set; }
}
public static class CustomerLocationExtensions
{
    public static CustomerLocationDto ToDto(this CustomerLocation customerLocation)
        => new CustomerLocationDto
        {
            CustomerLocationId = customerLocation.CustomerLocationId,
            Name = customerLocation.Name,
            Version = customerLocation.Version
        };
}

