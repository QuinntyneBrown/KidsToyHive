using System;

namespace KidsToyHive.Domain.Models
{
    public class HtmlContent: BaseModel
    {
        public Guid HtmlContentId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }        
    }
}
