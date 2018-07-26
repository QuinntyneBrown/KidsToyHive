using KidsToyHive.API.Features.Brands;
using KidsToyHive.Core.Models;
using System;

namespace KidsToyHive.API.Features.Products
{
    public class ProductDto
    {        
        public Guid ProductId { get; set; }
        public Guid BrandId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public static ProductDto FromProduct(Product product)
            => new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl
            };
    }
}
