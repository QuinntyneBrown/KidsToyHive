using MediatR;
using System.Threading.Tasks;
using System.Threading;
using FluentValidation;
using Infrastructure.Data;
using Core.Entities;

namespace ProductService.Features.Brands
{
    public class SaveBrandCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Brand.BrandId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public BrandApiModel Brand { get; set; }
        }

        public class Response
        {			
            public int BrandId { get; set; }
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
                var brand = await _context.Brands.FindAsync(request.Brand.BrandId);

                if (brand == null) _context.Brands.Add(brand = new Brand());

                brand.Name = request.Brand.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { BrandId = brand.BrandId };
            }
        }
    }
}
