using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Infrastructure.Data;
using FluentValidation;

namespace ProductService.Features.Brands
{
    public class GetBrandByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.BrandId).NotEqual(0);
            }
        }

        public class Request : IRequest<Response> {
            public int BrandId { get; set; }
        }

        public class Response
        {
            public BrandApiModel Brand { get; set; }
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
                    Brand = BrandApiModel.FromBrand(await _context.Brands.FindAsync(request.BrandId))
                };
        }
    }
}
