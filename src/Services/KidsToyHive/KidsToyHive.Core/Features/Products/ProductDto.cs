// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using KidsToyHive.Core.Enums;

namespace KidsToyHive.Core.Features.Products;

public class ProductDtoValidator : AbstractValidator<ProductDto>
{
    public ProductDtoValidator()
    {
        RuleFor(x => x.ProductId).NotNull();
        RuleFor(x => x.Name).NotNull();
        RuleFor(x => x.ChargePeriodPrice).NotNull();
    }
}
public class ProductDto
{
    public Guid ProductId { get; set; }
    public Guid? ProductCategoryId { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }
    public ICollection<ProductImageDto> ProductImages { get; set; }
        = new HashSet<ProductImageDto>();
    public int ChargePeriodPrice { get; set; }
    public ChargePeriod ChargePeriod { get; set; }
    public int Version { get; set; }
}
public static class ProductExtensions
{
    public static ProductDto ToDto(this Product product)
        => new ProductDto
        {
            ProductId = product.ProductId,
            ProductCategoryId = product.ProductCategoryId,
            Description = product.Description,
            Name = product.Name,
            ProductImages = product.ProductImages.Select(x => x.ToDto()).ToList(),
            Version = product.Version,
            ChargePeriodPrice = product.ChargePeriodPrice,
            ChargePeriod = product.ChargePeriod
        };
}

