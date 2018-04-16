using Core.Entities;

namespace ProductService.Features.Brands
{
    public class BrandApiModel
    {        
        public int BrandId { get; set; }
        public string Name { get; set; }

        public static BrandApiModel FromBrand(Brand brand)
        {
            var model = new BrandApiModel();
            model.BrandId = brand.BrandId;
            model.Name = brand.Name;
            return model;
        }
    }
}
