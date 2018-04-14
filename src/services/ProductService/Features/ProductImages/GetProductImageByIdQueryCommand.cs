using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Infrastructure.Data;
using FluentValidation;

namespace ProductService.Features.ProductImages
{
    public class GetProductImageByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ProductImageId).NotEqual(0);
            }
        }

        public class Request : IRequest<Response> {
            public int ProductImageId { get; set; }
        }

        public class Response
        {
            public ProductImageApiModel ProductImage { get; set; }
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
                    ProductImage = ProductImageApiModel.FromProductImage(await _context.ProductImages.FindAsync(request.ProductImageId))
                };
        }
    }
}
