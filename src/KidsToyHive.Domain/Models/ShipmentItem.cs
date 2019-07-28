using System;

namespace KidsToyHive.Domain.Models
{
    public class ShipmentItem: BaseModel
    {
        public Guid ShipmentItemId { get; set; }
        public Guid ShipmentId { get; set; }
        public Shipment Shipment { get; set; }
        public Guid? SalesOrderDetailId { get; set; }
        public Guid? BookingDetailId { get; set; }
        public int Quantity { get; set; }
        public string Comments { get; set; }
    }
}
