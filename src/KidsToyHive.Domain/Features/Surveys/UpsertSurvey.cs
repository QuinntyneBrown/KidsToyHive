using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Surveys;

public class UpsertSurveyValidator : AbstractValidator<UpsertSurveyRequest>
{
    public UpsertSurveyValidator()
    {
        RuleFor(request => request.Survey).NotNull();
        RuleFor(request => request.Survey).SetValidator(new SurveyDtoValidator());
    }
}
public class UpsertSurveyRequest : IRequest<UpsertSurveyResponse>
{
    public SurveyDto Survey { get; set; }
}
public class UpsertSurveyResponse
{
    public Guid SurveyId { get; set; }
}
public class UpsertSurveyHandler : IRequestHandler<UpsertSurveyRequest, UpsertSurveyResponse>
{
    public IAppDbContext _context { get; set; }
    public UpsertSurveyHandler(IAppDbContext context) => _context = context;
    public async Task<UpsertSurveyResponse> Handle(UpsertSurveyRequest request, CancellationToken cancellationToken)
    {
        var survey = await _context.Surveys.FindAsync(request.Survey.SurveyId);
        if (survey == null)
        {
            survey = new Survey();
            _context.Surveys.Add(survey);
        }
        survey.Name = request.Survey.Name;
        await _context.SaveChangesAsync(cancellationToken);
        return new UpsertSurveyResponse() { SurveyId = survey.SurveyId };
    }
}
