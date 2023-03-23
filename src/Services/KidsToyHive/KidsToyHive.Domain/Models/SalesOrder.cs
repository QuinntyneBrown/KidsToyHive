using KidsToyHive.Core.Enums;
using System;
using System.Collections.Generic;

namespace KidsToyHive.Domain.Models;

public class SalesOrder : BaseModel
{
    public Guid SalesOrderId { get; set; }
    public float Cost { get; set; }
    public SalesOrderStatus Status { get; set; } = SalesOrderStatus.New;

    public ICollection<SalesOrderDetail> SalesOrderDetails { get; set; }
        = new HashSet<SalesOrderDetail>();
}
