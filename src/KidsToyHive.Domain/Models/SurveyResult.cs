using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KidsToyHive.Domain.Models;

public class SurveyResult : BaseModel
{
    public Guid SurveyResultId { get; set; }
    [ForeignKey("Survey")]
    public Guid? SurveyId { get; set; }
    public string Name { get; set; }
    public ICollection<Response> Responses { get; set; }
        = new HashSet<Response>();
    public Survey Survey { get; set; }
}
