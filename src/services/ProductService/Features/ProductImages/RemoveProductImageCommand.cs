using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Infrastructure.Data;
using Core.Entities;
using FluentValidation;

namespace ProductService.Features.ProductImages
{
    public class RemoveProductImageCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ProductImage.ProductImageId).NotEqual(0);
            }
        }

        public class Request : IRequest
        {
            public ProductImage ProductImage { get; set; }
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
                _context.ProductImages.Remove(await _context.ProductImages.FindAsync(request.ProductImage.ProductImageId));
                await _context.SaveChangesAsync(cancellationToken);
            }

        }
    }
}
