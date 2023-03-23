using System;

namespace KidsToyHive.Domain.Models;

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
