using System;

namespace KidsToyHive.Domain.Models
{
    public class Card
    {
        public Guid CardId { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
    }
}
