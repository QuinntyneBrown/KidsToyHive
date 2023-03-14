using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KidsToyHive.Domain.Models;

public class ResponseResponse : BaseModel
{
    public Guid ResponseId { get; set; }
    [ForeignKey("Option")]
    public Guid? OptionId { get; set; }
    [ForeignKey("Question")]
    public Guid? QuestionId { get; set; }
    public string Value { get; set; }
    public Question Question { get; set; }
    public Option Option { get; set; }
}
