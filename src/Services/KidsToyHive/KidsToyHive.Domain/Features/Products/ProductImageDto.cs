using KidsToyHive.Domain.Models;
using FluentValidation;
using System;
using KidsToyHive.Domain.Features.DigitalAssets;

namespace KidsToyHive.Domain.Features.Products;

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
