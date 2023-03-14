using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.SalesOrders;

public class RemoveSalesOrder
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(request => request.SalesOrderId).NotNull();
        }
    }
    public class Request : IRequest
    {
        public Guid SalesOrderId { get; set; }
    }
    public class Handler : IRequestHandler<Request>
    {
        private readonly IAppDbContext _context;
        public Handler(IAppDbContext context) => _context = context;
        public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
        {
            var salesOrder = await _context.SalesOrders.FindAsync(request.SalesOrderId);
            _context.SalesOrders.Remove(salesOrder);
            await _context.SaveChangesAsync(cancellationToken);
            return new Unit();
        }
    }
}
