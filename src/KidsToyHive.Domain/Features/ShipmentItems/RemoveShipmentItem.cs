using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ShipmentItems;

public class Validator : AbstractValidator<Request>
{
    public Validator()
    {
        RuleFor(request => request.ShipmentItemId).NotNull();
    }
}
public class RemoveShipmentItemRequest : IRequest
{
    public Guid ShipmentItemId { get; set; }
}
public class RemoveShipmentItemHandler : IRequestHandler<Request>
{
    private readonly IAppDbContext _context;
    public RemoveShipmentItemHandler(IAppDbContext context) => _context = context;
    public async Task<Unit> Handle(RemoveShipmentItemRequest request, CancellationToken cancellationToken)
    {
        var shipmentItem = await _context.ShipmentItems.FindAsync(request.ShipmentItemId);
        _context.ShipmentItems.Remove(shipmentItem);
        await _context.SaveChangesAsync(cancellationToken);
        return new Unit();
    }
}
