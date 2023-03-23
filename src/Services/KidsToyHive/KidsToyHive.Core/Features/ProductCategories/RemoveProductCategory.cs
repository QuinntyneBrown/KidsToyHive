// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.ProductCategories;

public class RemoveProductCategoryValidator : AbstractValidator<RemoveProductCategoryRequest>
{
    public RemoveProductCategoryValidator()
    {
        RuleFor(request => request.ProductCategoryId).NotNull();
    }
}
public class RemoveProductCategoryRequest : IRequest
{
    public Guid ProductCategoryId { get; set; }
}
public class RemoveProductCategoryHandler : IRequestHandler<RemoveProductCategoryRequest>
{
    private readonly IKidsToyHiveDbContext _context;
    public RemoveProductCategoryHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task Handle(RemoveProductCategoryRequest request, CancellationToken cancellationToken)
    {
        var productCategory = await _context.ProductCategories.FindAsync(request.ProductCategoryId);
        _context.ProductCategories.Remove(productCategory);
        await _context.SaveChangesAsync(cancellationToken);

    }
}

