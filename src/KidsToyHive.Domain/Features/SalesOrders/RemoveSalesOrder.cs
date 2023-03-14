using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.SalesOrders;

public class RemoveSalesOrderValidator : AbstractValidator<RemoveSalesOrderRequest>
{
    public RemoveSalesOrderValidator()
    {
        RuleFor(request => request.SalesOrderId).NotNull();
    }
}
public class RemoveSalesOrderRequest : IRequest
{
    public Guid SalesOrderId { get; set; }
}
public class RemoveSalesOrderHandler : IRequestHandler<RemoveSalesOrderRequest>
{
    private readonly IAppDbContext _context;
    public RemoveSalesOrderHandler(IAppDbContext context) => _context = context;
    public async Task<Unit> Handle(RemoveSalesOrderRequest request, CancellationToken cancellationToken)
    {
        var salesOrder = await _context.SalesOrders.FindAsync(request.SalesOrderId);
        _context.SalesOrders.Remove(salesOrder);
        await _context.SaveChangesAsync(cancellationToken);
        return new Unit();
    }
}
