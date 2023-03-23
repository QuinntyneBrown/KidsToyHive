using System;

namespace KidsToyHive.Domain.Models;

public class Role
{
    public Guid RoleId { get; set; }
    public string Name { get; set; }
    public int Version { get; set; }
}
