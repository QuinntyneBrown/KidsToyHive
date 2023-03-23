// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;

namespace KidsToyHive.Core.Models;

public class Driver : BaseModel
{
    public Guid DriverId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public Guid? LocationId { get; set; }
    public Location Location { get; set; }
    public ICollection<Shipment> Shipments { get; set; }
    = new HashSet<Shipment>();
}

