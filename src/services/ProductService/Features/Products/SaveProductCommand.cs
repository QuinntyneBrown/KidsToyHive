using MediatR;
using System.Threading.Tasks;
using System.Threading;
using FluentValidation;
using Infrastructure.Data;
using Core.Entities;

namespace ProductService.Features.Products
{
    public class SaveProductCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Product.ProductId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public ProductApiModel Product { get; set; }
        }

        public class Response
        {			
            public int ProductId { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var product = await _context.Products.FindAsync(request.Product.ProductId);

                if (product == null) _context.Products.Add(product = new Product());

                product.Name = request.Product.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { ProductId = product.ProductId };
            }
        }
    }
}
