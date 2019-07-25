using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ProductCategories
{
    public class GetProductCategoryById
    {
        public class Request : IRequest<Response> {
            public Guid ProductCategoryId { get; set; }
        }

        public class Response
        {
            public ProductCategoryDto ProductCategory { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    ProductCategory = (await _context.ProductCategories.FindAsync(request.ProductCategoryId)).ToDto()
                };
        }
    }
}
