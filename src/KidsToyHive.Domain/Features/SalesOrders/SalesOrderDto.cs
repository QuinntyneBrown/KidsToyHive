using KidsToyHive.Domain.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Domain.Features.SalesOrders
{
    public class SalesOrderDtoValidator: AbstractValidator<SalesOrderDto>
    {
        public SalesOrderDtoValidator()
        {
            RuleFor(x => x.SalesOrderId).NotNull();
            RuleFor(x => x.Name).NotNull();
        }
    }

    public class SalesOrderDto
    {        
        public Guid SalesOrderId { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
    }

    public static class SalesOrderExtensions
    {        
        public static SalesOrderDto ToDto(this SalesOrder salesOrder)
            => new SalesOrderDto
            {
                SalesOrderId = salesOrder.SalesOrderId,
                Version = salesOrder.Version
            };
    }
}
