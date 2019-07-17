using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ProductCategories
{
    public class UpsertProductCategory
    {

        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.ProductCategory).NotNull();
                RuleFor(request => request.ProductCategory).SetValidator(new ProductCategoryDtoValidator());
            }
        }

        public class Request : IRequest<Response> {
            public ProductCategoryDto ProductCategory { get; set; }
        }

        public class Response
        {
            public Guid ProductCategoryId { get;set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                var productCategory = await _context.ProductCategories.FindAsync(request.ProductCategory.ProductCategoryId);

                if (productCategory == null) {
                    productCategory = new ProductCategory();
                    _context.ProductCategories.Add(productCategory);
                }

                productCategory.Name = request.ProductCategory.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { ProductCategoryId = productCategory.ProductCategoryId };
            }
        }
    }
}
