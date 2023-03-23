using System;

namespace KidsToyHive.Domain.Models;

public class Card : BaseModel
{
    public Guid CardId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
