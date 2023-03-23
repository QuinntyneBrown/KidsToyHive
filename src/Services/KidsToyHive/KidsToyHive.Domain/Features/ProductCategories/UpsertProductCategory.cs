using KidsToyHive.Domain;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.ProductCategories;

public class UpsertProductCategoryValidator : AbstractValidator<UpsertProductCategoryRequest>
{
    public UpsertProductCategoryValidator()
    {
        RuleFor(request => request.ProductCategory).NotNull();
        RuleFor(request => request.ProductCategory).SetValidator(new ProductCategoryDtoValidator());
    }
}
public class UpsertProductCategoryRequest : IRequest<UpsertProductCategoryResponse>
{
    public ProductCategoryDto ProductCategory { get; set; }
}
public class UpsertProductCategoryResponse
{
    public Guid ProductCategoryId { get; set; }
}
public class UpsertProductCategoryHandler : IRequestHandler<UpsertProductCategoryRequest, UpsertProductCategoryResponse>
{
    private readonly IAppDbContext _context;
    public UpsertProductCategoryHandler(IAppDbContext context) => _context = context;
    public async Task<UpsertProductCategoryResponse> Handle(UpsertProductCategoryRequest request, CancellationToken cancellationToken)
    {
        var productCategory = await _context.ProductCategories.FindAsync(request.ProductCategory.ProductCategoryId);
        if (productCategory == null)
        {
            productCategory = new ProductCategory();
            _context.ProductCategories.Add(productCategory);
        }
        productCategory.Name = request.ProductCategory.Name;
        await _context.SaveChangesAsync(cancellationToken);
        return new UpsertProductCategoryResponse() { ProductCategoryId = productCategory.ProductCategoryId };
    }
}
