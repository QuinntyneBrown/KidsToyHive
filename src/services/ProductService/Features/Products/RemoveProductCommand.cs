using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Infrastructure.Data;
using Core.Entities;
using FluentValidation;

namespace ProductService.Features.Products
{
    public class RemoveProductCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Product.ProductId).NotEqual(0);
            }
        }

        public class Request : IRequest
        {
            public Product Product { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task Handle(Request request, CancellationToken cancellationToken)
            {
                _context.Products.Remove(await _context.Products.FindAsync(request.Product.ProductId));
                await _context.SaveChangesAsync(cancellationToken);
            }

        }
    }
}
