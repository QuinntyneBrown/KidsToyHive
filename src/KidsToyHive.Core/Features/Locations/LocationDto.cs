// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Models;
using FluentValidation;
using System;
using KidsToyHive.Core.Features.Addresses;
using KidsToyHive.Core.Enums;

namespace KidsToyHive.Core.Features.Locations;

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

