using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Brands
{
    public class RemoveBrand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.BrandId).NotNull();
            }
        }

        public class Request: IRequest
        {
            public Guid BrandId { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                var brand = await _context.Brands.FindAsync(request.BrandId);

                _context.Brands.Remove(brand);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit();
            }
        }
    }
}
