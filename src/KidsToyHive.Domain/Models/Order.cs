using KidsToyHive.Core.Enums;
using System;
using System.Collections.Generic;

namespace KidsToyHive.Domain.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } 
            = new HashSet<OrderItem>();
        public Guid SignatureId { get; set; }
        public OrderSignature Signature { get; set; }
        public int Version { get; set; }
        public OrderState State { get; set; } = OrderState.New;
    }
}
