// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Products;

public class UpsertProductValidator : AbstractValidator<UpsertProductRequest>
{
    public UpsertProductValidator()
    {
        RuleFor(request => request.Product).NotNull();
        RuleFor(request => request.Product).SetValidator(new ProductDtoValidator());
    }
}
public class UpsertProductRequest : IRequest<UpsertProductResponse>
{
    public ProductDto Product { get; set; }
}
public class UpsertProductResponse
{
    public Guid ProductId { get; set; }
}
public class UpsertProductHandler : IRequestHandler<UpsertProductRequest, UpsertProductResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public UpsertProductHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<UpsertProductResponse> Handle(UpsertProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(request.Product.ProductId);
        if (product == null)
        {
            product = new Product();
            _context.Products.Add(product);
        }
        product.Name = request.Product.Name;
        await _context.SaveChangesAsync(cancellationToken);
        return new UpsertProductResponse() { ProductId = product.ProductId };
    }
}

