using System;

namespace KidsToyHive.Domain.Models
{
    public class DashboardCard
    {
        public Guid DashboardCardId { get; set; }
        public Guid DashboardId { get; set; }
        public string Options { get; set; }
        public Guid CardId { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }

    }
}
