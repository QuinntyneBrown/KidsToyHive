using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Surveys;

public class RemoveSurveyValidator : AbstractValidator<RemoveSurveyRequest>
{
    public RemoveSurveyValidator()
    {
        RuleFor(request => request.SurveyId).NotNull();
    }
}
public class RemoveSurveyRequest : IRequest
{
    public Guid SurveyId { get; set; }
}
public class RemoveSurveyHandler : IRequestHandler<RemoveSurveyRequest>
{
    private readonly IAppDbContext _context;
    public RemoveSurveyHandler(IAppDbContext context) => _context = context;
    public async Task<Unit> Handle(RemoveSurveyRequest request, CancellationToken cancellationToken)
    {
        var survey = await _context.Surveys.FindAsync(request.SurveyId);
        _context.Surveys.Remove(survey);
        await _context.SaveChangesAsync(cancellationToken);
        return new Unit();
    }
}
