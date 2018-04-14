using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Infrastructure.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProductService.Features.ProductCategories
{
    public class GetProductCategoriesQuery
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public IEnumerable<ProductCategoryApiModel> ProductCategories { get; set; }
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
                    ProductCategories = await _context.ProductCategories.Select(x => ProductCategoryApiModel.FromProductCategory(x)).ToListAsync()
                };
        }
    }
}
