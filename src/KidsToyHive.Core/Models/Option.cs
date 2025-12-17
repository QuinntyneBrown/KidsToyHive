// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KidsToyHive.Core.Models;

public class Option : BaseModel
{
    public Guid OptionId { get; set; }
    [ForeignKey("Question")]
    public Guid? QuestionId { get; set; }
    public ICollection<Response> Responses { get; set; }
        = new HashSet<Response>();
    public int Order { get; set; }
    public string Name { get; set; }
    public Question Question { get; set; }
}

