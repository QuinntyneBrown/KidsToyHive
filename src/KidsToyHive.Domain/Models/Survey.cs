using System;
using System.Collections.Generic;

namespace KidsToyHive.Domain.Models;

public class Survey : BaseModel
{
    public Guid SurveyId { get; set; }
    public string Name { get; set; }
    public ICollection<Question> Questions { get; set; }
    = new HashSet<Question>();
    public ICollection<SurveyResult> SurveyResults { get; set; }
        = new HashSet<SurveyResult>();
}
