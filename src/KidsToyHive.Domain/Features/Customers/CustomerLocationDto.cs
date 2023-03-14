using KidsToyHive.Domain.Models;
using FluentValidation;
using System;
using KidsToyHive.Domain.Features.Locations;

namespace KidsToyHive.Domain.Features.Customers;

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
