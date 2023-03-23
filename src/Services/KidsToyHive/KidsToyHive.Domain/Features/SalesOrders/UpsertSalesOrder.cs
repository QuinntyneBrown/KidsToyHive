using KidsToyHive.Domain;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.SalesOrders;

public class UpsertSalesOrderValidator : AbstractValidator<UpsertSalesOrderRequest>
{
    public UpsertSalesOrderValidator()
    {
        RuleFor(request => request.SalesOrder).NotNull();
        RuleFor(request => request.SalesOrder).SetValidator(new SalesOrderDtoValidator());
    }
}
public class UpsertSalesOrderRequest : IRequest<UpsertSalesOrderResponse>
{
    public SalesOrderDto SalesOrder { get; set; }
}
public class UpsertSalesOrderResponse
{
    public Guid SalesOrderId { get; set; }
}
public class UpsertSalesOrderHandler : IRequestHandler<UpsertSalesOrderRequest, UpsertSalesOrderResponse>
{
    private readonly IAppDbContext _context;
    public UpsertSalesOrderHandler(IAppDbContext context) => _context = context;
    public async Task<UpsertSalesOrderResponse> Handle(UpsertSalesOrderRequest request, CancellationToken cancellationToken)
    {
        var salesOrder = await _context.SalesOrders.FindAsync(request.SalesOrder.SalesOrderId);
        if (salesOrder == null)
        {
            salesOrder = new SalesOrder();
            _context.SalesOrders.Add(salesOrder);
        }
        await _context.SaveChangesAsync(cancellationToken);
        return new UpsertSalesOrderResponse() { SalesOrderId = salesOrder.SalesOrderId };
    }
}
