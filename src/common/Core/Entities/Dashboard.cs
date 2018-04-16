using System.Collections.Generic;

namespace Core.Entities
{
    public class Dashboard: BaseEntity
    {
        public int DashboardId { get; set; }           
		public string Name { get; set; }
        public ICollection<DashboardCard> DashboardCards { get; set; } = new HashSet<DashboardCard>();        
    }
}
