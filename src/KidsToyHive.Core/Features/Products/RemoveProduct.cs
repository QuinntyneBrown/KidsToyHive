// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Products;

public class RemoveProductValidator : AbstractValidator<RemoveProductRequest>
{
    public RemoveProductValidator()
    {
        RuleFor(request => request.ProductId).NotNull();
    }
}
public class RemoveProductRequest : IRequest
{
    public Guid ProductId { get; set; }
}
public class RemoveProductHandler : IRequestHandler<RemoveProductRequest>
{
    private readonly IKidsToyHiveDbContext _context;
    public RemoveProductHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task Handle(RemoveProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(request.ProductId);
        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);

    }
}

