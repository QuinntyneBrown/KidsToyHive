namespace Core.Entities
{
    public class ProductImage: BaseEntity
    {
        public int ProductImageId { get; set; }           
		public string Url { get; set; }        
        public string Description { get; set; }
    }
}
