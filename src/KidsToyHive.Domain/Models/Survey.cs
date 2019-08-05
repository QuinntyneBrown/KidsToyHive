using System;

namespace KidsToyHive.Domain.Models
{
    public class Survey: BaseModel
    {
        public Guid SurveyId { get; set; }
        public string Name { get; set; }        
    }
}
