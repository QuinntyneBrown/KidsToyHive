using KidsToyHive.Domain.Models;
using FluentValidation;
using System;
using KidsToyHive.Domain.Features.Addresses;
using KidsToyHive.Core.Enums;

namespace KidsToyHive.Domain.Features.Locations;

public class LocationDtoValidator : AbstractValidator<LocationDto>
{
    public LocationDtoValidator()
    {
    }
}
public class LocationDto
{
    public Guid? LocationId { get; set; }
    public AddressDto Address { get; set; }
    public LocationType Type { get; set; }
    public int Version { get; set; }
}
public static class LocationExtensions
{
    public static LocationDto ToDto(this Location location)
        => new LocationDto
        {
            LocationId = location.LocationId,
            Address = location.Adddress.ToDto(),
            Version = location.Version
        };
}
