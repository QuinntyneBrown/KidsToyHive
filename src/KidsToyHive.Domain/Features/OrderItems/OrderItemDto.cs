using KidsToyHive.Domain.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Domain.Features.OrderItems
{
    public class OrderItemDtoValidator: AbstractValidator<OrderItemDto>
    {
        public OrderItemDtoValidator()
        {
            RuleFor(x => x.OrderItemId).NotNull();
            RuleFor(x => x.Name).NotNull();
        }
    }

    public class OrderItemDto
    {        
        public Guid OrderItemId { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
    }

    public static class OrderItemExtensions
    {        
        public static OrderItemDto ToDto(this OrderItem orderItem)
            => new OrderItemDto
            {
                OrderItemId = orderItem.OrderItemId,
                Name = orderItem.Name,
                Version = orderItem.Version
            };
    }
}
