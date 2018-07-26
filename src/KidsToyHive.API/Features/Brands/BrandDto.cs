using KidsToyHive.Core.Models;
using System;

namespace KidsToyHive.API.Features.Brands
{
    public class BrandDto
    {        
        public Guid BrandId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public static BrandDto FromBrand(Brand brand)
            => new BrandDto
            {
                BrandId = brand.BrandId,
                Name = brand.Name
            };
    }
}
