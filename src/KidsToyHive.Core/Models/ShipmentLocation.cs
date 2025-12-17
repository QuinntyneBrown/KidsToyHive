// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace KidsToyHive.Core.Models;

public class ShipmentLocation
{
    public Guid ShipmentId { get; set; }
    public Guid? LocationId { get; set; }
    public Shipment Shipment { get; set; }
    public Location Location { get; set; }
}

