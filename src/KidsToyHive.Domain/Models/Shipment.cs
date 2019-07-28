using KidsToyHive.Core.Enums;
using System;
using System.Collections.Generic;

namespace KidsToyHive.Domain.Models
{
    public class Shipment: BaseModel
    {
        public Guid ShipmentId { get; set; }        
        public string TrackingNumber { get; set; }
        public decimal? TotalWeight { get; set; }
        public Guid DriverId { get; set; }
        public Guid? LocationId { get; set; }             
        public Guid SignatureId { get; set; }
        public Driver Driver { get; set; }
        public Location Location { get; set; }
        public Signature Signature { get; set; }        
        public string Comment { get; set; }
        public ShipmentType Type { get; set; } = ShipmentType.Delivery;
        public ShipmentStatus Status { get; set; } = ShipmentStatus.New;
        public ICollection<ShipmentItem> ShipmentItems { get; set; }
            = new HashSet<ShipmentItem>();
        public ICollection<ShipmentBooking> ShipmentBookings { get; set; } 
            = new HashSet<ShipmentBooking>();
        public ICollection<ShipmentSalesOrder> ShipmentSalesOrders { get; set; }
            = new HashSet<ShipmentSalesOrder>();

    }
}
