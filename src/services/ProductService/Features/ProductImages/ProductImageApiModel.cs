using Core.Entities;

namespace ProductService.Features.ProductImages
{
    public class ProductImageApiModel
    {        
        public int ProductImageId { get; set; }
        
        public static ProductImageApiModel FromProductImage(ProductImage productImage)
        {
            var model = new ProductImageApiModel();
            model.ProductImageId = productImage.ProductImageId;
            
            return model;
        }
    }
}
