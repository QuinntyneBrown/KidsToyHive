using KidsToyHive.Domain;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.SalesOrderDetails;

public class RemoveSalesOrderDetailValidator : AbstractValidator<RemoveSalesOrderDetailRequest>
{
    public RemoveSalesOrderDetailValidator()
    {
        RuleFor(request => request.SalesOrderDetailId).NotNull();
    }
}
public class RemoveSalesOrderDetailRequest : IRequest
{
    public Guid SalesOrderDetailId { get; set; }
}
public class RemoveSalesOrderDetailHandler : IRequestHandler<RemoveSalesOrderDetailRequest>
{
    private readonly IAppDbContext _context;
    public RemoveSalesOrderDetailHandler(IAppDbContext context) => _context = context;
    public async Task<Unit> Handle(RemoveSalesOrderDetailRequest request, CancellationToken cancellationToken)
    {
        var salesOrderDetail = await _context.SalesOrderDetails.FindAsync(request.SalesOrderDetailId);
        _context.SalesOrderDetails.Remove(salesOrderDetail);
        await _context.SaveChangesAsync(cancellationToken);
        return new Unit();
    }
}