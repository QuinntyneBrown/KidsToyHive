using System;

namespace KidsToyHive.Domain.Models
{
    public class Question: BaseModel
    {
        public Guid QuestionId { get; set; }
        public string Name { get; set; }
    }
}
