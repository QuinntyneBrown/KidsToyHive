using KidsToyHive.Domain;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ShipmentItems;

public class UpsertShipmentItemValidator : AbstractValidator<UpsertShipmentItemRequest>
{
    public UpsertShipmentItemValidator()
    {
        RuleFor(request => request.ShipmentItem).NotNull();
        RuleFor(request => request.ShipmentItem).SetValidator(new ShipmentItemDtoValidator());
    }
}
public class UpsertShipmentItemRequest : IRequest<UpsertShipmentItemResponse>
{
    public ShipmentItemDto ShipmentItem { get; set; }
}
public class UpsertShipmentItemResponse
{
    public Guid ShipmentItemId { get; set; }
}
public class UpsertShipmentItemHandler : IRequestHandler<UpsertShipmentItemRequest, UpsertShipmentItemResponse>
{
    public IAppDbContext _context { get; set; }
    public UpsertShipmentItemHandler(IAppDbContext context) => _context = context;
    public async Task<UpsertShipmentItemResponse> Handle(UpsertShipmentItemRequest request, CancellationToken cancellationToken)
    {
        var shipmentItem = await _context.ShipmentItems.FindAsync(request.ShipmentItem.ShipmentItemId);
        if (shipmentItem == null)
        {
            shipmentItem = new ShipmentItem();
            _context.ShipmentItems.Add(shipmentItem);
        }
        await _context.SaveChangesAsync(cancellationToken);
        return new UpsertShipmentItemResponse() { ShipmentItemId = shipmentItem.ShipmentItemId };
    }
}
