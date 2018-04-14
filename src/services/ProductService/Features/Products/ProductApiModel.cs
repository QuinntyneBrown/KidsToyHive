using Core.Entities;

namespace ProductService.Features.Products
{
    public class ProductApiModel
    {        
        public int ProductId { get; set; }
        public string Name { get; set; }

        public static ProductApiModel FromProduct(Product product)
        {
            var model = new ProductApiModel();
            model.ProductId = product.ProductId;
            model.Name = product.Name;
            return model;
        }
    }
}
