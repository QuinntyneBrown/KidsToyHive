// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Core.Features.ProductCategories;

public class ProductCategoryDtoValidator : AbstractValidator<ProductCategoryDto>
{
    public ProductCategoryDtoValidator()
    {
        RuleFor(x => x.ProductCategoryId).NotNull();
        RuleFor(x => x.Name).NotNull();
    }
}
public class ProductCategoryDto
{
    public Guid ProductCategoryId { get; set; }
    public string Name { get; set; }
    public int Version { get; set; }
}
public static class ProductCategoryExtensions
{
    public static ProductCategoryDto ToDto(this ProductCategory productCategory)
        => new ProductCategoryDto
        {
            ProductCategoryId = productCategory.ProductCategoryId,
            Name = productCategory.Name,
            Version = productCategory.Version
        };
}

