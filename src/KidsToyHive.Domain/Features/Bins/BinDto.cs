using System;

namespace KidsToyHive.Domain.Features.Bins;

public class BinDto
{
    public Guid BinId { get; set; }
    public string Name { get; set; }
    public int Version { get; set; }
}
