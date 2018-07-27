using KidsToyHive.Core.Models;
using System;

namespace KidsToyHive.API.Features.Orders
{
    public class OrderItemDto
    {        
        public Guid OrderItemId { get; set; }
        public string Name { get; set; }

        public static OrderItemDto FromOrderItem(OrderItem orderItem)
            => new OrderItemDto
            {
                OrderItemId = orderItem.OrderItemId,
                Name = orderItem.Name
            };
    }
}
