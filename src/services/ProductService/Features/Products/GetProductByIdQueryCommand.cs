using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Infrastructure.Data;
using FluentValidation;

namespace ProductService.Features.Products
{
    public class GetProductByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ProductId).NotEqual(0);
            }
        }

        public class Request : IRequest<Response> {
            public int ProductId { get; set; }
        }

        public class Response
        {
            public ProductApiModel Product { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    Product = ProductApiModel.FromProduct(await _context.Products.FindAsync(request.ProductId))
                };
        }
    }
}
