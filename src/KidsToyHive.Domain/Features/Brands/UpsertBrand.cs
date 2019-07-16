using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Brands
{
    public class UpsertBrand
    {

        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Brand).NotNull();
                RuleFor(request => request.Brand).SetValidator(new BrandDtoValidator());
            }
        }

        public class Request : IRequest<Response> {
            public BrandDto Brand { get; set; }
        }

        public class Response
        {
            public Guid BrandId { get;set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                var brand = await _context.Brands.FindAsync(request.Brand.BrandId);

                if (brand == null) {
                    brand = new Brand();
                    _context.Brands.Add(brand);
                }

                brand.Name = request.Brand.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { BrandId = brand.BrandId };
            }
        }
    }
}
