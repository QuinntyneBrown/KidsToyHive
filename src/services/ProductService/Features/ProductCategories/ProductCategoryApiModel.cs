using Core.Entities;

namespace ProductService.Features.ProductCategories
{
    public class ProductCategoryApiModel
    {        
        public int ProductCategoryId { get; set; }
        public string Name { get; set; }

        public static ProductCategoryApiModel FromProductCategory(ProductCategory productCategory)
        {
            var model = new ProductCategoryApiModel();
            model.ProductCategoryId = productCategory.ProductCategoryId;
            model.Name = productCategory.Name;
            return model;
        }
    }
}
