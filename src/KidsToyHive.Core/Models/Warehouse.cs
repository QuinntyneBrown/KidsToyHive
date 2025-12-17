// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;

namespace KidsToyHive.Core.Models;

public class Warehouse : BaseModel
{
    public Guid WarehouseId { get; set; }
    public string Name { get; set; }
    public Guid? LocationId { get; set; }
    public Location Location { get; set; }
    public ICollection<Bin> Bins { get; set; }
    = new HashSet<Bin>();
    public ICollection<InventoryItem> InventoryItems { get; set; }
    = new HashSet<InventoryItem>();
}

