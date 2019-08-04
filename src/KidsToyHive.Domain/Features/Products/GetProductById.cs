using KidsToyHive.Domain.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    Product = (await _context.Products
                    .Include(x => x.ProductCategory)
                    .Include(x => x.ProductImages)
                    .ThenInclude(x => x.DigitalAsset)
                    .SingleAsync(x => x.ProductId == request.ProductId)).ToDto()
                };
        }
    }
}
