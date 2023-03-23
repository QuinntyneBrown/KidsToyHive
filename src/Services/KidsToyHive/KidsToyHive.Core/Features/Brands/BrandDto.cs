// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using KidsToyHive.Core.Features.Products;

namespace KidsToyHive.Core.Features.Brands;

public class BrandDtoValidator : AbstractValidator<BrandDto>
{
    public BrandDtoValidator()
    {
        RuleFor(x => x.BrandId).NotNull();
        RuleFor(x => x.Name).NotNull();
    }
}
public class BrandDto
{
    public Guid BrandId { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public ICollection<ProductDto> Products { get; set; }
    = new HashSet<ProductDto>();
}
public static class BrandExtensions
{
    public static BrandDto ToDto(this Brand brand)
        => new BrandDto
        {
            BrandId = brand.BrandId,
            Name = brand.Name
        };
}

