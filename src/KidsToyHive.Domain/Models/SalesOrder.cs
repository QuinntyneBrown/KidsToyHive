using System;
using System.Collections.Generic;

namespace KidsToyHive.Domain.Models
{
    public class SalesOrder: BaseModel
    {
        public Guid SalesOrderId { get; set; }
        public ICollection<SalesOrderDetail> SalesOrderDetails { get; set; } 
            = new HashSet<SalesOrderDetail>();        
    }
}
