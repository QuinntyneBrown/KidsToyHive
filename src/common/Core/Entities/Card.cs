namespace Core.Entities
{
    public class Card: BaseEntity
    {
        public int CardId { get; set; }           
		public string Name { get; set; }
        public string Description { get; set; }
    }
}
