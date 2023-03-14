using KidsToyHive.Domain.DataAccess;
using KidsToyHive.Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain.Features.Surveys;

public class UpsertSurvey
{
    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(request => request.Survey).NotNull();
            RuleFor(request => request.Survey).SetValidator(new SurveyDtoValidator());
        }
    }
    public class Request : IRequest<Response>
    {
        public SurveyDto Survey { get; set; }
    }
    public class Response
    {
        public Guid SurveyId { get; set; }
    }
    public class Handler : IRequestHandler<Request, Response>
    {
        public IAppDbContext _context { get; set; }
        public Handler(IAppDbContext context) => _context = context;
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var survey = await _context.Surveys.FindAsync(request.Survey.SurveyId);
            if (survey == null)
            {
                survey = new Survey();
                _context.Surveys.Add(survey);
            }
            survey.Name = request.Survey.Name;
            await _context.SaveChangesAsync(cancellationToken);
            return new Response() { SurveyId = survey.SurveyId };
        }
    }
}
