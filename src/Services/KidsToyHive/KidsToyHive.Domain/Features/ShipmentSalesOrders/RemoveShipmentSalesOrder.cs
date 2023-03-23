using KidsToyHive.Domain;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ShipmentSalesOrders;

public class RemoveShipmentSalesOrderValidator : AbstractValidator<RemoveShipmentSalesOrderRequest>
{
    public RemoveShipmentSalesOrderValidator()
    {
        RuleFor(request => request.ShipmentSalesOrderId).NotNull();
    }
}
public class RemoveShipmentSalesOrderRequest : IRequest
{
    public Guid ShipmentSalesOrderId { get; set; }
}
public class RemoveShipmentSalesOrderHandler : IRequestHandler<RemoveShipmentSalesOrderRequest>
{
    private readonly IAppDbContext _context;
    public RemoveShipmentSalesOrderHandler(IAppDbContext context) => _context = context;
    public async Task<Unit> Handle(RemoveShipmentSalesOrderRequest request, CancellationToken cancellationToken)
    {
        var shipmentSalesOrder = await _context.ShipmentSalesOrders.FindAsync(request.ShipmentSalesOrderId);
        _context.ShipmentSalesOrders.Remove(shipmentSalesOrder);
        await _context.SaveChangesAsync(cancellationToken);
        return new Unit();
    }
}
