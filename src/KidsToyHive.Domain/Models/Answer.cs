using System;

namespace KidsToyHive.Domain.Models
{
    public class Answer: BaseModel
    {
        public Guid AnswerId { get; set; }
        public string Name { get; set; }        
    }
}
