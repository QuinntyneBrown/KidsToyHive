using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Infrastructure.Data;
using FluentValidation;

namespace ProductService.Features.ProductCategories
{
    public class GetProductCategoryByIdQuery
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ProductCategoryId).NotEqual(0);
            }
        }

        public class Request : IRequest<Response> {
            public int ProductCategoryId { get; set; }
        }

        public class Response
        {
            public ProductCategoryApiModel ProductCategory { get; set; }
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
                    ProductCategory = ProductCategoryApiModel.FromProductCategory(await _context.ProductCategories.FindAsync(request.ProductCategoryId))
                };
        }
    }
}
