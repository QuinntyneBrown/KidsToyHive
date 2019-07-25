using KidsToyHive.Domain.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Domain.Features.ShipmentSalesOrders
{
    public class ShipmentSalesOrderDtoValidator: AbstractValidator<ShipmentSalesOrderDto>
    {
        public ShipmentSalesOrderDtoValidator()
        {
            RuleFor(x => x.ShipmentSalesOrderId).NotNull();
            RuleFor(x => x.Name).NotNull();
        }
    }

    public class ShipmentSalesOrderDto
    {        
        public Guid ShipmentSalesOrderId { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
    }

    public static class ShipmentSalesOrderExtensions
    {        
        public static ShipmentSalesOrderDto ToDto(this ShipmentSalesOrder shipmentSalesOrder)
            => new ShipmentSalesOrderDto
            {
                ShipmentSalesOrderId = shipmentSalesOrder.ShipmentSalesOrderId,
                Name = shipmentSalesOrder.Name,
                Version = shipmentSalesOrder.Version
            };
    }
}
