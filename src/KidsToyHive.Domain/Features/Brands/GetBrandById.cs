using KidsToyHive.Domain.DataAccess;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Brands
{
    public class GetBrandById
    {
        public class Request : IRequest<Response> {
            public Guid BrandId { get; set; }
        }

        public class Response
        {
            public BrandDto Brand { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
                => new Response()
                {
                    Brand = (await _context.Brands.FindAsync(request.BrandId)).ToDto()
                };
        }
    }
}
