using MediatR;
using System.Threading.Tasks;
using System.Threading;
using FluentValidation;
using Infrastructure.Data;
using Core.Entities;

namespace ProductService.Features.ProductImages
{
    public class SaveProductImageCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.ProductImage.ProductImageId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public ProductImageApiModel ProductImage { get; set; }
        }

        public class Response
        {			
            public int ProductImageId { get; set; }
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
                var productImage = await _context.ProductImages.FindAsync(request.ProductImage.ProductImageId);

                if (productImage == null) _context.ProductImages.Add(productImage = new ProductImage());
                
                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { ProductImageId = productImage.ProductImageId };
            }
        }
    }
}
