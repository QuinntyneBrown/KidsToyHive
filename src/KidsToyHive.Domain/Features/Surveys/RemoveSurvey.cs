using KidsToyHive.Domain.DataAccess;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Surveys;

public class RemoveSurvey
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(request => request.SurveyId).NotNull();
        }
    }
    public class Request : IRequest
    {
        public Guid SurveyId { get; set; }
    }
    public class Handler : IRequestHandler<Request>
    {
        private readonly IAppDbContext _context;
        public Handler(IAppDbContext context) => _context = context;
        public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
        {
            var survey = await _context.Surveys.FindAsync(request.SurveyId);
            _context.Surveys.Remove(survey);
            await _context.SaveChangesAsync(cancellationToken);
            return new Unit();
        }
    }
}
