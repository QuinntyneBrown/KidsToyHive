// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core;
using KidsToyHive.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Core.Features.Brands;

public class UpsertBrandValidator : AbstractValidator<UpsertBrandRequest>
{
    public UpsertBrandValidator()
    {
        RuleFor(request => request.Brand).NotNull();
        RuleFor(request => request.Brand).SetValidator(new BrandDtoValidator());
    }
}
public class UpsertBrandRequest : IRequest<UpsertBrandResponse>
{
    public BrandDto Brand { get; set; }
}
public class UpsertBrandResponse
{
    public Guid BrandId { get; set; }
}
public class UpsertBrandHandler : IRequestHandler<UpsertBrandRequest, UpsertBrandResponse>
{
    private readonly IKidsToyHiveDbContext _context;
    public UpsertBrandHandler(IKidsToyHiveDbContext context) => _context = context;
    public async Task<UpsertBrandResponse> Handle(UpsertBrandRequest request, CancellationToken cancellationToken)
    {
        var brand = await _context.Brands.FindAsync(request.Brand.BrandId);
        if (brand == null)
        {
            brand = new Brand();
            _context.Brands.Add(brand);
        }
        brand.Name = request.Brand.Name;
        await _context.SaveChangesAsync(cancellationToken);
        return new UpsertBrandResponse() { BrandId = brand.BrandId };
    }
}

