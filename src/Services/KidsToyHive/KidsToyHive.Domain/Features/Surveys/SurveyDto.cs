using KidsToyHive.Domain.Models;
using FluentValidation;
using System;

namespace KidsToyHive.Domain.Features.Surveys;

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
