// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace KidsToyHive.Core.Models;

public class CustomerLocation : BaseModel
{
    public Guid CustomerLocationId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid? LocationId { get; set; }
    public Customer Customer { get; set; }
    public Location Location { get; set; }
    public string Name { get; set; }
}

