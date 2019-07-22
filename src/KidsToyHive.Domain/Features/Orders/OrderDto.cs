using FluentValidation;
using KidsToyHive.Core.Enums;
using KidsToyHive.Domain.Features.OrderItems;
using KidsToyHive.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KidsToyHive.Domain.Features.Orders
{
    public class OrderDtoValidator: AbstractValidator<OrderDto>
    {
        public OrderDtoValidator()
        {
            RuleFor(x => x.OrderId).NotNull();
            RuleFor(x => x.State).NotNull();
            RuleForEach(x => x.OrderItems).SetValidator(new OrderItemDtoValidator());
        }
    }

    public class OrderDto
    {        
        public Guid OrderId { get; set; }
        public OrderState State { get; set; } = OrderState.New;
        public ICollection<OrderItemDto> OrderItems = new HashSet<OrderItemDto>();
        public int Version { get; set; }
    }

    public static class OrderExtensions
    {        
        public static OrderDto ToDto(this Order order)
            => new OrderDto
            {
                OrderId = order.OrderId,
                State = order.State,
                OrderItems = order.OrderItems.Select(x => x.ToDto()).ToList(),
                Version = order.Version
            };
    }
}
