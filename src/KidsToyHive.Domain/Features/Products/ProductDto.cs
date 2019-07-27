using KidsToyHive.Domain.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Domain.Features.Products
{
    public class ProductDtoValidator: AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(x => x.ProductId).NotNull();
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.HourlyRate).NotNull();
        }
    }

    public class ProductDto
    {        
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int HourlyRate { get; set; }
        public int Version { get; set; }
    }

    public static class ProductExtensions
    {        
        public static ProductDto ToDto(this Product product)
            => new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Version = product.Version,
                HourlyRate = product.HourlyRate
            };
    }
}
