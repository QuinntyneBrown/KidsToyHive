// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Models;
using FluentValidation;
using System;
using KidsToyHive.Core.Features.DigitalAssets;

namespace KidsToyHive.Core.Features.Products;

public class ProductImageDtoValidator : AbstractValidator<ProductImageDto>
{
    public ProductImageDtoValidator()
    {
        RuleFor(x => x.ProductImageId).NotNull();
        RuleFor(x => x.Name).NotNull();
    }
}
public class ProductImageDto
{
    public Guid ProductImageId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public int Version { get; set; }
}
public static class ProductImageExtensions
{
    public static ProductImageDto ToDto(this ProductImage productImage)
        => new ProductImageDto
        {
            ProductImageId = productImage.ProductImageId,
            Url = productImage.Url,
            Version = productImage.Version
        };
}

