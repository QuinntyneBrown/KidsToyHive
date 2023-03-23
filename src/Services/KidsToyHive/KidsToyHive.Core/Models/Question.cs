// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KidsToyHive.Core.Models;

public class Question : BaseModel
{
    public Guid QuestionId { get; set; }
    [ForeignKey("Survey")]
    public Guid? SurveyId { get; set; }
    public QuestionType QuestionType { get; set; } = QuestionType.Default;
    public string Name { get; set; }
    public string Body { get; set; }
    public string Description { get; set; }
    public int Order { get; set; }
    public Survey Survey { get; set; }
    public ICollection<Option> Options { get; set; }
        = new HashSet<Option>();
}

