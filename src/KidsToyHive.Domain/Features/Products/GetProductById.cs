using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Products
{
    public class GetProductById
    {
        public class Request : IRequest<Response> {
            public Guid ProductId { get; set; }
        }

        public class Response
        {
            public ProductDto Product { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    Product = (await _context.Products.FindAsync(request.ProductId)).ToDto()
                };
        }
    }
}
