// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.ProductCategories;

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
    private readonly IKidsToyHiveDbContext _context;
    public UpsertProductCategoryHandler(IKidsToyHiveDbContext context) => _context = context;
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

