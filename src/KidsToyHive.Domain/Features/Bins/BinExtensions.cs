using KidsToyHive.Domain.Models;

namespace KidsToyHive.Domain.Features.Bins;

public static class BinExtensions
{
    public static BinDto ToDto(this Bin bin)
        => new BinDto
        {
            BinId = bin.BinId,
            Name = bin.Name,
            Version = bin.Version
        };
}
