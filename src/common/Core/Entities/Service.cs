namespace Core.Entities
{
    public class Service: BaseEntity
    {
        public int ServiceId { get; set; }           
		public string Name { get; set; }
        public string Description { get; set; }
    }
}
