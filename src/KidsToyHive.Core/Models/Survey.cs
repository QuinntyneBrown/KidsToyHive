// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;

namespace KidsToyHive.Core.Models;

public class Survey : BaseModel
{
    public Guid SurveyId { get; set; }
    public string Name { get; set; }
    public ICollection<Question> Questions { get; set; }
    = new HashSet<Question>();
    public ICollection<SurveyResult> SurveyResults { get; set; }
        = new HashSet<SurveyResult>();
}

