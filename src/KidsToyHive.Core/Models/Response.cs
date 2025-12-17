// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KidsToyHive.Core.Models;

public class Response : BaseModel
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

