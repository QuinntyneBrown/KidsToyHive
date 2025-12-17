// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using GeoCoordinatePortable;

namespace KidsToyHive.Core.Interfaces;

public interface IGeoCoordinate
{
    public double Latitude { get; }
    public double Longitude { get; }
    public GeoCoordinate GeoCoordinate => new GeoCoordinate(Latitude, Longitude);
    public double GetDistanceTo(IGeoCoordinate destination)
        => GeoCoordinate.GetDistanceTo(destination.GeoCoordinate);
}

