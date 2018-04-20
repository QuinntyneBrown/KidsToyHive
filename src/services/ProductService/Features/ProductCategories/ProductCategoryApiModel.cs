using Core.Entities;

namespace ProductService.Features.ProductCategories
{
    public class ProductCategoryApiModel
    {        
        public int ProductCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public static ProductCategoryApiModel FromProductCategory(ProductCategory productCategory)
        {
            var model = new ProductCategoryApiModel
            {
                ProductCategoryId = productCategory.ProductCategoryId,
                Name = productCategory.Name,
                Description = productCategory.Description,
                ImageUrl = productCategory.ImageUrl
            };
            return model;
        }
    }
}
