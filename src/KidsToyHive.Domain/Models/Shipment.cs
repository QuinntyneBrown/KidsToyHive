using KidsToyHive.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KidsToyHive.Domain.Models
{
    public class Shipment: BaseModel
    {
        public Guid ShipmentId { get; set; }        
        public string TrackingNumber { get; set; }
        public double TotalWeight { get; set; }
        [ForeignKey("Driver")]
        public Guid? DriverId { get; set; }
        [ForeignKey("Location")]
        public Guid? LocationId { get; set; }
        [ForeignKey("Signature")]
        public Guid? SignatureId { get; set; }        
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
