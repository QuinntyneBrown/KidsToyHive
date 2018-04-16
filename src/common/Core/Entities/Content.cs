namespace Core.Entities
{
    public class Content: BaseEntity
    {
        public int ContentId { get; set; }           
		public string Name { get; set; }
        public string HtmlBody { get; set; }
    }
}
