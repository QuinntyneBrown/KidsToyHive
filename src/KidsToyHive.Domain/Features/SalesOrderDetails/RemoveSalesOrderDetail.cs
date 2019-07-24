using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.SalesOrderDetails
{
    public class RemoveSalesOrderDetail
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.SalesOrderDetailId).NotNull();
            }
        }

        public class Request: IRequest
        {
            public Guid SalesOrderDetailId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                var salesOrderDetail = await _context.SalesOrderDetails.FindAsync(request.SalesOrderDetailId);

                _context.SalesOrderDetails.Remove(salesOrderDetail);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit();
            }
        }
    }
}
