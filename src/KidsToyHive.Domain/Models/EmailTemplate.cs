using System;

namespace KidsToyHive.Domain.Models
{
    public class EmailTemplate
    {
        public Guid EmailTemplateId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
