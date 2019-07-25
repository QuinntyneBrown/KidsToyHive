using KidsToyHive.Domain.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Domain.Features.Shipments
{
    public class ShipmentDtoValidator: AbstractValidator<ShipmentDto>
    {
        public ShipmentDtoValidator()
        {
            RuleFor(x => x.ShipmentId).NotNull();
        }
    }

    public class ShipmentDto
    {        
        public Guid ShipmentId { get; set; }
        public int Version { get; set; }
    }

    public static class ShipmentExtensions
    {        
        public static ShipmentDto ToDto(this Shipment shipment)
            => new ShipmentDto
            {
                ShipmentId = shipment.ShipmentId,
                Version = shipment.Version
            };
    }
}
