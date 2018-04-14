using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Infrastructure.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProductService.Features.ProductImages
{
    public class GetProductImagesQuery
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public IEnumerable<ProductImageApiModel> ProductImages { get; set; }
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
                    ProductImages = await _context.ProductImages.Select(x => ProductImageApiModel.FromProductImage(x)).ToListAsync()
                };
        }
    }
}
