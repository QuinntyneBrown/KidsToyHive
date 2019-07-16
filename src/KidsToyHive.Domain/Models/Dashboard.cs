using System;
using System.Collections.Generic;

namespace KidsToyHive.Domain.Models
{
    public class Dashboard
    {
        public Guid DashboardId { get; set; }
        public string Name { get; set; }
        public ICollection<DashboardCard> Cards { get; set; } = new HashSet<DashboardCard>();
        public int Version { get; set; }
    }
}
