using System;

namespace KidsToyHive.Core.DomainEvents
{
    public class DashboardCardAddedToDashboard: DomainEvent
    {
        public DashboardCardAddedToDashboard(Guid dashboardCardId)
            => DashboardCardId = dashboardCardId;
        
        public Guid DashboardCardId { get; set; }
    }
}
