using System;
using System.Collections.Generic;

namespace KidsToyHive.Domain.Models;

public class Bin : BaseModel
{
    public Guid BinId { get; set; }
    public string Name { get; set; }
    public Guid WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }
    public ICollection<InventoryItem> InventoryItems { get; set; }
        = new HashSet<InventoryItem>();
}
