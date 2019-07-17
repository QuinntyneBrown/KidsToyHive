using KidsToyHive.Domain.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Domain.Features.Orders
{
    public class OrderDtoValidator: AbstractValidator<OrderDto>
    {
        public OrderDtoValidator()
        {
            RuleFor(x => x.OrderId).NotNull();
            RuleFor(x => x.Name).NotNull();
        }
    }

    public class OrderDto
    {        
        public Guid OrderId { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
    }

    public static class OrderExtensions
    {        
        public static OrderDto ToDto(this Order order)
            => new OrderDto
            {
                OrderId = order.OrderId,
                Name = order.Name,
                Version = order.Version
            };
    }
}
