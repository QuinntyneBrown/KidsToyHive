using FluentValidation;
using KidsToyHive.Core.Enums;
using KidsToyHive.Domain.Features.ShipmentItems;
using KidsToyHive.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public string TrackingNumber { get; set; }
        public decimal? TotalWeight { get; set; }
        public Guid DriverId { get; set; }
        public Guid? LocationId { get; set; }
        public Guid SignatureId { get; set; }
        public Driver Driver { get; set; }
        public Location Location { get; set; }
        public Signature Signature { get; set; }
        public ShipmentType Type { get; set; }
        public ShipmentStatus Status { get; set; }
        public string Comment { get; set; }
        public ICollection<ShipmentItemDto> ShipmentItems { get; set; }
        = new HashSet<ShipmentItemDto>();
        public int Version { get; set; }
    }

    public static class ShipmentExtensions
    {        
        public static ShipmentDto ToDto(this Shipment shipment)
            => new ShipmentDto
            {
                ShipmentId = shipment.ShipmentId,
                TrackingNumber = shipment.TrackingNumber,
                TotalWeight = shipment.TotalWeight,
                DriverId = shipment.DriverId,
                LocationId = shipment.LocationId,
                Type = shipment.Type,
                Status = shipment.Status,
                ShipmentItems = shipment.ShipmentItems
                .Select(x => x.ToDto())
                .ToList(),
                Version = shipment.Version
            };
    }
}
