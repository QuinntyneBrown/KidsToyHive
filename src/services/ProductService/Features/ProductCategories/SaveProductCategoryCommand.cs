using MediatR;
using System.Threading.Tasks;
using System.Threading;
using FluentValidation;
using Infrastructure.Data;
using Core.Entities;

namespace ProductService.Features.ProductCategories
{
    public class SaveProductCategoryCommand
    {
        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.ProductCategory.ProductCategoryId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public ProductCategoryApiModel ProductCategory { get; set; }
        }

        public class Response
        {			
            public int ProductCategoryId { get; set; }
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
                var productCategory = await _context.ProductCategories.FindAsync(request.ProductCategory.ProductCategoryId);

                if (productCategory == null) _context.ProductCategories.Add(productCategory = new ProductCategory());

                productCategory.Name = request.ProductCategory.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { ProductCategoryId = productCategory.ProductCategoryId };
            }
        }
    }
}
