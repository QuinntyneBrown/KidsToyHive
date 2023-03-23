// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Brands;

public class RemoveBrandValidator : AbstractValidator<RemoveBrandRequest>
{
    public RemoveBrandValidator()
    {
        RuleFor(request => request.BrandId).NotNull();
    }
}
public class RemoveBrandRequest : IRequest
{
    public Guid BrandId { get; set; }
}
public class RemoveBrandHandler : IRequestHandler<RemoveBrandRequest>
{
    private readonly IKidsToyHiveDbContext _context;
    public RemoveBrandHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task Handle(RemoveBrandRequest request, CancellationToken cancellationToken)
    {
        var brand = await _context.Brands.FindAsync(request.BrandId);
        _context.Brands.Remove(brand);
        await _context.SaveChangesAsync(cancellationToken);

    }
}

