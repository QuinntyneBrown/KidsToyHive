using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Infrastructure.Data;
using Core.Entities;
using FluentValidation;

namespace ProductService.Features.Brands
{
    public class RemoveBrandCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.Brand.BrandId).NotEqual(0);
            }
        }

        public class Request : IRequest
        {
            public Brand Brand { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task Handle(Request request, CancellationToken cancellationToken)
            {
                _context.Brands.Remove(await _context.Brands.FindAsync(request.Brand.BrandId));
                await _context.SaveChangesAsync(cancellationToken);
            }

        }
    }
}
