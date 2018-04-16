namespace Core.Entities
{
    public class DashboardCard: BaseEntity
    {
        public int DashboardCardId { get; set; }      
        public string Options { get; set; }
        public int DashboardId { get; set; }
        public int CardId { get; set; }
        public Dashboard Dashboard { get; set; }
        public Card Card { get; set; }
    }
}
