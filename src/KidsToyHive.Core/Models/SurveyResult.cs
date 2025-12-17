// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KidsToyHive.Core.Models;

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

