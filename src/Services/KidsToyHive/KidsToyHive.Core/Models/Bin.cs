// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;

namespace KidsToyHive.Core.Models;

public class Bin : BaseModel
{
    public Guid BinId { get; set; }
    public string Name { get; set; }
    public Guid WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }
    public ICollection<InventoryItem> InventoryItems { get; set; }
        = new HashSet<InventoryItem>();
}

