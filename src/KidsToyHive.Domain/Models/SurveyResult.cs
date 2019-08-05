using System;

namespace KidsToyHive.Domain.Models
{
    public class SurveyResult: BaseModel
    {
        public Guid SurveyResultId { get; set; }
        public string Name { get; set; }        
    }
}
