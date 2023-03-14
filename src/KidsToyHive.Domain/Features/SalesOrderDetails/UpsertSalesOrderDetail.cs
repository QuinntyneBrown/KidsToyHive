using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.SalesOrderDetails;

public class UpsertSalesOrderDetail
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(request => request.SalesOrderDetail).NotNull();
            RuleFor(request => request.SalesOrderDetail).SetValidator(new SalesOrderDetailDtoValidator());
        }
    }
    public class Request : IRequest<Response>
    {
        public SalesOrderDetailDto SalesOrderDetail { get; set; }
    }
    public class Response
    {
        public Guid SalesOrderDetailId { get; set; }
    }
    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly IAppDbContext _context;
        public Handler(IAppDbContext context) => _context = context;
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var salesOrderDetail = await _context.SalesOrderDetails.FindAsync(request.SalesOrderDetail.SalesOrderDetailId);
            if (salesOrderDetail == null)
            {
                salesOrderDetail = new SalesOrderDetail();
                _context.SalesOrderDetails.Add(salesOrderDetail);
            }
            salesOrderDetail.Name = request.SalesOrderDetail.Name;
            await _context.SaveChangesAsync(cancellationToken);
            return new Response() { SalesOrderDetailId = salesOrderDetail.SalesOrderDetailId };
        }
    }
}
