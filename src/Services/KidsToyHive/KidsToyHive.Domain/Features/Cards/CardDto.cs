using System;

namespace KidsToyHive.Domain.Features.Cards;
public class CardDto
{
    public Guid CardId { get; set; }
    public string Name { get; set; }
    public int Version { get; set; }
}
