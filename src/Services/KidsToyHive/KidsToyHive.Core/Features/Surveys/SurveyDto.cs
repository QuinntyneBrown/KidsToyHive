// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using KidsToyHive.Core.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Core.Features.Surveys;

public class SurveyDtoValidator : AbstractValidator<SurveyDto>
{
    public SurveyDtoValidator()
    {
        RuleFor(x => x.SurveyId).NotNull();
        RuleFor(x => x.Name).NotNull();
    }
}
public class SurveyDto
{
    public Guid SurveyId { get; set; }
    public string Name { get; set; }
    public int Version { get; set; }
}
public static class SurveyExtensions
{
    public static SurveyDto ToDto(this Survey survey)
        => new SurveyDto
        {
            SurveyId = survey.SurveyId,
            Name = survey.Name,
            Version = survey.Version
        };
}

