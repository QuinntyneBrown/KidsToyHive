// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace KidsToyHive.Core.Models;

public class InventoryItem : BaseModel
{
    public Guid InventoryItemId { get; set; }
    public Guid ProductId { get; set; }
    public Guid? WarehouseId { get; set; }
    public Guid? BinId { get; set; }
    public int Quantity { get; set; }
    public DateTime Modified { get; set; }
    public Warehouse Warehouse { get; set; }
    public Product Product { get; set; }
    public Bin Bin { get; set; }
}

