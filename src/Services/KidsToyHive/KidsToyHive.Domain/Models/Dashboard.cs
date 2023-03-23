using System;
using System.Collections.Generic;

namespace KidsToyHive.Domain.Models;

public class Dashboard
{
    public Guid DashboardId { get; set; }
    public string Name { get; set; }
    public Guid ProfileId { get; set; }
    public Profile Profile { get; set; }
    public string Options { get; set; }
    public ICollection<DashboardCard> DashboardCards { get; set; }
        = new HashSet<DashboardCard>();
    public int Version { get; set; }
}
