using System;

namespace KidsToyHive.Domain.Models;

public class ShipmentSalesOrder
{
    public Guid ShipmentSalesOrderId { get; set; }
    public Guid ShipmentId { get; set; }
    public Guid SalesOrderId { get; set; }
    public string Name { get; set; }
    public int Version { get; set; }
}
