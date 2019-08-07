using System;

namespace KidsToyHive.Domain.Models
{
    public class ContactMessage
    {
        public Guid ContactMessageId { get; set; }        
        public string Value { get; set; }
        public int Version { get; set; }
    }
}
