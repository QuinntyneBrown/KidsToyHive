using System;
using System.Collections.Generic;

namespace KidsToyHive.Domain.Models;

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
