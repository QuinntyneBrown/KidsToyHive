using KidsToyHive.Core.Models;
using System;

namespace KidsToyHive.API.Features.Orders
{
    public class OrderDto
    {        
        public Guid OrderId { get; set; }
        public string Name { get; set; }

        public static OrderDto FromOrder(Order order)
            => new OrderDto
            {
                OrderId = order.OrderId,
                Name = order.Name
            };
    }
}
