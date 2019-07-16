using System;

namespace KidsToyHive.Domain.Models
{
    public class InventoryItem
    {
        public Guid InventoryItemId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public int Version { get; set; }
    }
}
