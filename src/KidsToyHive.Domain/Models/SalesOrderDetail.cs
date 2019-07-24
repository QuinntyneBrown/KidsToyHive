using System;

namespace KidsToyHive.Domain.Models
{
    public class SalesOrderDetail
    {
        public Guid SalesOrderDetailId { get; set; }

        public string Name { get; set; }
        public int Version { get; set; }
    }
}
