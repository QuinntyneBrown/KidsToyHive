using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ProductCategories
{
    public class GetProductCategories
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public IEnumerable<ProductCategoryDto> ProductCategories { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                =>  new Response()
                {
                    ProductCategories = await _context.ProductCategories.Select(x => x.ToDto()).ToArrayAsync()
                };
        }
    }
}
