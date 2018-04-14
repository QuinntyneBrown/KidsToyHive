using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Infrastructure.Data;
using Core.Entities;
using FluentValidation;

namespace ProductService.Features.ProductCategories
{
    public class RemoveProductCategoryCommand
    {
        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(request => request.ProductCategory.ProductCategoryId).NotEqual(0);
            }
        }

        public class Request : IRequest
        {
            public ProductCategory ProductCategory { get; set; }
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
                _context.ProductCategories.Remove(await _context.ProductCategories.FindAsync(request.ProductCategory.ProductCategoryId));
                await _context.SaveChangesAsync(cancellationToken);
            }

        }
    }
}
