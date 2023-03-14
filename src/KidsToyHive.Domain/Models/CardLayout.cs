using System;

namespace KidsToyHive.Domain.Models;

public class CardLayout
{
    public Guid CardLayoutId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Version { get; set; }
}
